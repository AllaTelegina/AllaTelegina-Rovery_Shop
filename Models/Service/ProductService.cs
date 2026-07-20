using System.Text.Json;
using Backend_asp.net.Interface;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Enams;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repo;
        public ProductService(IRepository repo)
        {
            _repo= repo;
        }

        //The method prepares the model for download page, where the catalog a product;
        public async Task<ProductsGeneral> ModelSendInViews()
        {
            // General property for send in views;
            var productGeneral = new ProductsGeneral(); 
            var products = await _repo.GetAllProductList();
            foreach (var prod in products)
            {
                var jsModel = new ModelJsAlpine<Product>
                {
                    id = prod.Id,
                    title = prod.Name,
                    sku = prod.SkuNumber,
                    price = prod.Prise,
                    oldPrice = prod.Lastprise,
                    rating = (double)prod.Rating,
                    status = StatusProduct.Name[prod.Status],
                    //status=prod.Status,
                    shortDesc = prod.BriefDiscription,
                    fullDesc = prod.FullDiscription,
                    statusInt = (int)prod.Status
                };
                jsModel.Create_time = prod.CreateRoverMechTime.ToString("dd.MM.yyyy (HH:mm)");
                // Date chenge:
                if (prod.Time_of_chenge == null)
                    jsModel.Time_of_chenge = " ";
                else
                {
                    jsModel.Time_of_chenge = prod.Time_of_chenge.Value.ToString("dd.MM.yyyy (HH:mm)");
                }
                // Features;
                ICollection<string> listfeat = new List<string>();
                foreach (var feat in prod.ProductKeyFeatures)
                {
                    if (feat.KeyFeature != null)
                        listfeat.Add(feat.KeyFeature.Name);
                }
                jsModel.keyFeatures = listfeat.ToArray();
                // Assigning a main category to a model;
                foreach (var category in prod.ProductCategoryes)
                {
                    if (category != null)
                    {
                        if (category.Category == null)
                            throw new Exception("Не найдена главная категория");
                        if (category.Category.LevelCategory == 0)
                        {
                            jsModel.category = category.Category.Name;
                            jsModel.categoryId = category.Category.Id;
                        }
                    }
                }
                // Assigning photo for the model;
                if (prod.ProductPicturees.Any())
                {
                    jsModel.pictures = new string[prod.ProductPicturees.Count];
                    var listArr = new List<string>();
                    foreach (var foto in prod.ProductPicturees)
                    {
                        listArr.Add(foto.Putch);
                    }
                    jsModel.pictures = listArr.ToArray();
                }

                //TODO: Following, i'm looking for all the categorys to a pproducts;
                var cateory_in_List = prod.ProductCategoryes.ToList()
                    .Where(t=>t.Category!=null && t.Category.LevelCategory>0)
                    .GroupBy(t => t.Category?.Title).ToList();  //This is the entire list of categories.;

                // already ready list;
                jsModel.specs = cateory_in_List.Where(t => t.Key != null)
                    .Select(t => new Specs()
                    {
                        name = t.Key!,
                        features = t.Where(g => g.Category is { Name: not null, Description: not null })
                        .Select(a => new Features()
                        {
                            key = a.Category!.Name,
                            value = a.Category.Description!
                        }).ToArray()
                    }).ToArray();
                productGeneral.modelJsAlpines.Add(jsModel);
            }
            // Added categories in page admin product, where level Category==0;
            productGeneral.ListCategorys = await _repo.GetListCategoryForPage();
            //TODO: it's temporary;
            return productGeneral;
        }

        public async Task <Product> JsonSerialFromJS(string StringFromJS)
        {
            var modelJsAlpine = new ModelJsAlpine<object>();
            try
            {
                modelJsAlpine = JsonSerializer.Deserialize<ModelJsAlpine<object>>(StringFromJS) ?? new ModelJsAlpine<object>();
            }
            //Проверка modelJsAlpine на NULL;
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в сериализации, {ex.Message}");
            }
            var productAfterSerial = new Product()
            {
                Name = modelJsAlpine.title,
                SkuNumber = modelJsAlpine.sku,
                Prise = modelJsAlpine.price,
                Lastprise = modelJsAlpine.oldPrice,
                Rating = (decimal)modelJsAlpine.rating,
                FullDiscription = modelJsAlpine.fullDesc,
                BriefDiscription = modelJsAlpine.shortDesc
            };
            // Crate status for object product;
            if (Enum.TryParse(modelJsAlpine.status, out StatusProductEnum result))
            {
                productAfterSerial.Status = result;
            }

            // Assigning the product main category;
            if (!int.TryParse(modelJsAlpine.category, out int id))
                throw new Exception("Not was create a main category of the products.");
            productAfterSerial.IndicateCategory= id;
            var productCategory = new ProductCategory()
            {
                Category = await _repo.GetMainCategory(id),
                Product = productAfterSerial,
            };




            return productAfterSerial;
        }
    }
}
