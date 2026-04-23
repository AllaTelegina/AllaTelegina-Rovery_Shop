using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend_asp.net.Controllers
{
    public class ShoppController : Controller
    {
        // context для подключения к BD;
        private readonly AplicationContext _context;

        // конструктор, который будет принимать context;
        public ShoppController(AplicationContext context)
        {
            _context = context;
        }

        // TODO: в этом методе передаю в представление List моделей ModelJsAlpine;
        public async Task  <IActionResult> Shoppmethod()
        {

            //      1. создать пустой лист для передачи;                    --> выполнено
            //      2. из базы данных добавить все продукты;                --> выполнено
            //      3. передать в модель, которая передает в представление
            //      4. передать в представление;

            ICollection<ModelJsAlpine<Product>> model_for_catalog = new List<ModelJsAlpine<Product>>();

            // Собираю модель из BD;
            var products = await _context.products
                .Include(t => t.ProductCategoryes)
                .Include(t => t.ProductPicturees)
                .Include(t => t.ProductKeyFeatures)
                .ToListAsync();
            // Перезапишу модель для js-модели page katalog.cshtml
            for (int i = 0; i < products.Count; i++)
            {
                ModelJsAlpine<Product> modelJs = new ModelJsAlpine<Product>();
                modelJs.Id = products[i].Id;
                modelJs.title = products[i].Name;
                var categoryName = await _context.categorys.FirstOrDefaultAsync(t => t.Id == products[i].IndicateCategory);
                if (categoryName == null)
                {
                    throw new Exception($"Категория {categoryName} не существует в базе данных");
                }
                modelJs.category = categoryName.Name;
                modelJs.price = products[i].Prise;
                modelJs.rating = (double)products[i].Rating;
                modelJs.sku = products[i].SkuNumber;
                //List<ProductPicture> prod = new List<ProductPicture>();
                var prod =products[i].ProductPicturees.Select(q=>q.Putch).ToList();
                modelJs.pictures=prod.ToArray();
                model_for_catalog.Add(modelJs);
            }
            ProductsGeneral product = new ProductsGeneral();
            product.modelJsAlpines=model_for_catalog.ToList();
            return View("katalog", product);
        }

        // TODO: Переходит из katalog.cshtml in card_product.cshtml;
        public async Task<IActionResult> KartProductStart(int id)
        {
            ProductsGeneral productGeneral = new ProductsGeneral();
            List<string> keyfeatures = new List<string>();
            var product = await _context.products.Include(t => t.ProductCategoryes)
                .Include(t => t.ProductPicturees)
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(key => key.KeyFeature)
                .Include(t=>t.ProductCategoryes)
                .ThenInclude(cat=>cat.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (product == null)
                throw new Exception($"Категория с {id} не найдена в базе данных");
            productGeneral.ProductFromAdmin = product;
            foreach (var a in product.ProductKeyFeatures)
            {
                if (a.KeyFeature == null)               
                {
                    ModelState.AddModelError("", "Не найден товар");
                    return View("Error");
                }
                productGeneral.Text.Add(a.KeyFeature.Name);
            }
            foreach (var image in product.ProductPicturees)
                productGeneral.ProductFromAdmin.Images.Add(image.Putch);

            // TODO: добовляю в productGeneral свойство modelJsAlpines
            List<ProductCategory> productCategories = new List<ProductCategory>();
            List<Category> categor = new List<Category>();
            ModelJsAlpine<Product> model = new ModelJsAlpine <Product> ();
            List <Features> features= new List<Features>();

            productCategories = product.ProductCategoryes.ToList();

            // список объектов Specs();
            List<Specs> listspec = new List<Specs>();

            for (int i = 0; i < productCategories.Count; i++)
            {
                // если уровень этой категории не равен "0";
                // TODO: нжуно собрать в один массив все категории где будет одинаковое имя и сделать массив с другими одинаковыми именами
                var prodcat = productCategories[i].Category;

                if (prodcat == null)
                {
                    throw new Exception($"no found object Category is null");
                    //return View("Error");
                }
                if (prodcat.LevelCategory != 0)
                {
                    // Хочу получить модель в которой будет одно свойство и массив свойств;
                    var prodcatnull = productCategories[i - 1].Category;
                    if (prodcatnull == null)
                        throw new Exception($"No object {prodcatnull}");
                    if (prodcat.Title != prodcatnull.Title)
                    {
                        var spec = new Specs();
                        if (prodcat.Title == null)
                        {
                            throw new Exception($"no found object Category is null");
                            //return View("Error");
                        }
                        spec.name = prodcat.Title;
                        var feate = new Features();
                        feate.key = prodcat.Name;
                        if (prodcat.Description == null)
                        {
                            throw new Exception($"no found object Category is null");
                            //return View("Error");
                        }
                        feate.value = prodcat.Description;
                        spec.features = spec.features.Append(feate).ToArray();
                        listspec.Add(spec);
                    }
                    else
                    {
                        var feate = new Features(); 
                        feate.key = prodcat.Name;
                        if (prodcat.Description == null)
                        {
                            throw new Exception($"no found object Category is null");
                            //return View("Error");
                        }
                        feate.value = prodcat.Description;
                        var lastelement = listspec.Last();
                        lastelement.features = lastelement.features.Append(feate).ToArray();
                        listspec[listspec.Count - 1] = lastelement;
                    }
                }
            }
            model.specs = listspec.ToArray();
            productGeneral.modelJsAlpines.Add(model);
            return View("card_produkt", productGeneral);
        }
    }
}
