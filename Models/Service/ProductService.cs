using System.Text.Json;
using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Enams;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;

namespace Backend_asp.net.Models.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repo;
        private readonly AplicationContext _contextServices;
        private readonly IWebHostEnvironment _inv;
        public ProductService(IRepository repo, AplicationContext context, IWebHostEnvironment inv)
        {
            _repo = repo;
            _contextServices = context;
            _inv = inv;
        }

        //        //The method prepares the model for download page, where the catalog a product;
        public async Task<ProductsGeneral> ModelSendInViews()
        {
            var productGeneral = new ProductsGeneral();
            productGeneral.MainCategories = await _repo.GetMainCategoryFromDB();
            var listJsModel = new List<ModelJsAlpine<Product>>();
            var products = await _repo.GetAllProductList();
            foreach(var product in products)
            {
                var jsmod = new ModelJsAlpine<Product>()
                {
                    id = product.Id,
                    title = product.Name,
                    category = product.MainCategory.Id.ToString(),
                    sku = product.SkuNumber,
                    price = product.Prise,
                    oldPrice = product.Lastprise,
                    rating = (double)product.Rating,
                    shortDesc = product.BriefDiscription,
                    fullDesc = product.FullDiscription,
                    categoryId = product.MainCategoryId,
                    statusInt = product.Status.ToString(),
                    status = StatusProduct.Name.First(t => t.Key == product.Status).Value,
                    createdAt = product.CreateRoverMechTime.ToString("dd/MM/yy/HH:mm"),
                    images =product.ProductPicturees.Select(t=>t.Putch).ToList()
                };
                foreach(var keyfeat in product.ProductKeyFeatures)
                {
                    if (keyfeat.KeyFeature != null)
                    {
                        string keyfeature = keyfeat.KeyFeature.Name;
                        jsmod.keyFeatures.Add(keyfeature);
                    }
                }
                jsmod.specs = product.ProductCategoryes.Select(prodcat => new Specs
                {
                    name = prodcat.Category!.Name,
                    features = prodcat.Category.PropertyTypeValues
                    .Select(val => new Features()
                    {
                        key = val.PropertyType.Name,
                        value = val.Value
                    }).ToList()
                }).ToList();
                listJsModel.Add(jsmod);
            }
            productGeneral.modelJsAlpines = listJsModel;
            return productGeneral;
        }

        // The method overiders from JSON to ModelJsAlpine 
        public ModelJsAlpine<Object> JsonSerialFromJS(string StringFromJS)
        {
            var productAfterSerial = JsonSerializer.Deserialize<ModelJsAlpine<Object>>(StringFromJS);
            if (productAfterSerial == null)
                throw new Exception("Ошибка в сериализации");
            return productAfterSerial;
        }

        // A method that adds product in to DB;
        public async Task <ProductsGeneral> AddNewProduct(string Json, List<IFormFile> imagesFiles)
        {
            var productgeneral = new ProductsGeneral();
            var modeljs=JsonSerialFromJS(Json);
            var product = new Product()
            {
                Name = modeljs.title,
                MainCategoryId = modeljs.categoryId,
                SkuNumber = modeljs.sku,
                Prise = modeljs.price,
                Lastprise = modeljs.oldPrice,
                BriefDiscription = modeljs.shortDesc,
                FullDiscription = modeljs.fullDesc,
                Rating = (decimal)modeljs.rating,
            };
            //filling the "ProductkeyFeature";
            foreach (string a in modeljs.keyFeatures)
            {
                var keyfeature = new KeyFeature()
                {
                    Name=a
                };
                var productkeyfeat = new ProductKeyFeature()
                {
                    KeyFeature=keyfeature,
                    Product=product
                };
                await _contextServices.keyFeatures.AddAsync(keyfeature);
                await _contextServices.productKeyFeatures.AddAsync(productkeyfeat);
            }
            //filling the "Status"
            product.Status = StatusProduct.Name.First(t => t.Value == modeljs.status).Key;
            //filling the "categorys"
            foreach(var category in modeljs.specs)
            {
                var categorProduct = new Category()
                {
                    Name=category.name,
                };
                foreach(var propertyType in category.features)
                {
                    var propertyTypeTable = new PropertyType()
                    {
                        Name=propertyType.key
                    };
                    var propertyTypeValue = new PropertyTypeValue()
                    {
                        Value = propertyType.value,
                        PropertyType=propertyTypeTable,
                        Category= categorProduct
                    };
                    _contextServices.PropertyTypeValues.Add(propertyTypeValue);
                    _contextServices.PropertyTypes.Add(propertyTypeTable);
                }
                var productCategory = new ProductCategory()
                {
                    Category = categorProduct,
                    Product = product
                };
                _contextServices.productCategories.Add(productCategory);
            }
            // Adding files to the DB.
            var strimFiles = imagesFiles.ToList();
            var mainFile = true;
            foreach (var file in strimFiles)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fileSaveAdressDirectory = Path.Combine(_inv.WebRootPath, "FotoProduct1");
                if(!Directory.Exists(fileSaveAdressDirectory))
                    Directory.CreateDirectory(fileSaveAdressDirectory);
                var fullFileAdress = Path.Combine(fileSaveAdressDirectory, filename);
                using (var filestream = new FileStream(fullFileAdress, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                };
                var photo = new Photoproduct()
                {
                    NamePicture = filename,
                    Putch = $"/FotoProduct1/{filename}",
                    Product = product
                };
                if (mainFile == true)
                    photo.MainPhoto = true;
                mainFile= false;
                await _contextServices.productsPicture.AddAsync(photo);
            }
            await _contextServices.SaveChangesAsync();
            return productgeneral;
        }

        // The method for chenges a model on the page;
        public async Task<ProductsGeneral> ChengeModel(ModelJsAlpine<object> modelJsAlpine, List<IFormFile> imagesFiles)
        {
            int id = (int)modelJsAlpine.id;
            var productgeneral = new ProductsGeneral();
            await _repo.GetProductById((int)modelJsAlpine.id);




            return  productgeneral;
        }
    }
}
