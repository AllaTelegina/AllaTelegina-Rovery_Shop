using System.ClientModel.Primitives;
using System.Text.Json;
using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //Определиние _context а также и конструктор для получения среды разработки;
        #region
        // Открытие базы данных, определение context;
        private readonly AplicationContext _context;
        // получаю путь из среды разработки папок;
        private readonly IWebHostEnvironment _inv;

        // Конструктор для создания контекста;
        public ProductController(AplicationContext context, IWebHostEnvironment inv)
        {
            _context = context;
            _inv = inv;
        }
        #endregion

        //---------------------------------<< Методы контроллера  >>------------------------------------//
        // Для загрузки этого представления;
        #region
        public async Task <IActionResult> ProductsView()
        {
            var product = new ProductsGeneral();
            product.ProductFromAdmin = new Product();
            product.ListCategorys=await _context.categorys.ToListAsync();
            return View("~/Areas/Admin/Views/Products/index.cshtml", product);
        }
        #endregion

        // Добовления товара;
        #region
        [HttpPost]
        public async Task<IActionResult> SaveProductForAdmin(ProductsGeneral productparameter, int IsCategory)
        {
            if(productparameter==null || productparameter.ProductFromAdmin==null)
                return BadRequest("Send error data.");
            var product = productparameter.ProductFromAdmin;
            // Для объекта product определим Category IsCategory;
            var categoryFromIsCategory = await _context.categorys.FirstOrDefaultAsync(a => a.Id==IsCategory);
            if (categoryFromIsCategory == null)
                return NotFound($"Категория с Id {IsCategory} не найдена");
            // Добовление в базу данных главной категории;
            #region
            ProductCategory productCategory = new();
            productCategory.CategoryId= categoryFromIsCategory.Id;
            product.ProductCategoryes.Add(productCategory);
            #endregion

            return RedirectToAction("ProductsView");
        }
        #endregion

        // Метод, который будет вызван при отправки из запроса .js
        #region
        // Для формирования и присвоения для продукта SKU;
        [HttpGet("SendSKU/{id}")]
        public async Task<IActionResult> SendSKU(int id)
        {
            var category= await _context.categorys.FirstOrDefaultAsync(c => c.Id==id);
            if (category == null)
                return NotFound("Not found category.");
            switch (category.Name)
            {
                case "Велосипеды":
                {
                    try
                    {
                        // пример для велосипеды -             "1000000"
                        var skuname = 0;
                        var product = await _context.products.OrderBy(x=>x.SkuNumber).LastOrDefaultAsync();
                        if (product != null)
                        {

                            if (int.TryParse(product.SkuNumber, out int x))
                            {
                                skuname = x;
                            }
                            // Добавить условие при котором, если не получится Parse числа; #дописать;
                            product.SkuNumber = (skuname++).ToString();
                            return Ok(product);
                        }
                        else
                        {
                            product = new Product();
                            product.SkuNumber= 1000001.ToString();
                            return Ok(product);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка выполнения \n" + ex);
                        break;
                    }
                }
                case "Электровелосипеды":
                    {
                        try
                        {
                            // пример для электровелосипедов -             "2000000"
                            var skuname = 0;
                            var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
                            if (product != null)
                            {

                                if (int.TryParse(product.SkuNumber, out int x))
                                {
                                    skuname = x;
                                }
                                // Добавить условие при котором, если не получится Parse числа; #дописать;
                                product.SkuNumber = (skuname++).ToString();
                                return Ok(product);
                            }
                            else
                            {
                                product = new Product();
                                product.SkuNumber = 2000001.ToString();
                                return Ok(product);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка выполнения \n" + ex);
                            break;
                        }
                    }
                case "Cамокаты":
                    {
                        try
                        {
                            // пример для самокаты -             "3000000"
                            var skuname = 0;
                            var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
                            if (product != null)
                            {

                                if (int.TryParse(product.SkuNumber, out int x))
                                {
                                    skuname = x;
                                }
                                // Добавить условие при котором, если не получится Parse числа; #дописать;
                                product.SkuNumber = (skuname++).ToString();
                                return Ok(product);
                            }
                            else
                            {
                                product = new Product();
                                product.SkuNumber = 3000001.ToString();
                                return Ok(product);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка выполнения \n" + ex);
                            break;
                        }
                    }
                case "Электросамокаты":
                    {
                        try
                        {
                            // пример для электросамокаты -             "4000000"
                            var skuname = 0;
                            var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
                            if (product != null)
                            {

                                if (int.TryParse(product.SkuNumber, out int x))
                                {
                                    skuname = x;
                                }
                                // Добавить условие при котором, если не получится Parse числа; #дописать;
                                product.SkuNumber = (skuname++).ToString();
                                return Ok(product);
                            }
                            else
                            {
                                product = new Product();
                                product.SkuNumber = 4000001.ToString();
                                return Ok(product);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка выполнения \n" + ex);
                            break;
                        }
                    }
                case "Аксессуары":
                    {
                        try
                        {
                            // пример для аксессуары -             "5000000"
                            var skuname = 0;
                            var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
                            if (product != null)
                            {

                                if (int.TryParse(product.SkuNumber, out int x))
                                {
                                    skuname = x;
                                }
                                // Добавить условие при котором, если не получится Parse числа; #дописать;
                                product.SkuNumber = (skuname++).ToString();
                                return Ok(product);
                            }
                            else
                            {
                                product = new Product();
                                product.SkuNumber = 5000001.ToString();
                                return Ok(product);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка выполнения \n" + ex);
                            break;
                        }
                    }
            }
            return Ok();
        }
        #endregion

        // Метод для добовления товара черз API JS;
        [HttpPost]
        public async Task <IActionResult> AddProductJS(
            [FromForm] string keyObjectProduct, [FromForm] List<IFormFile> imagesFiles)
        {
            ModelJsAlpine<object>? modelJsAlpine = null;
            Product product = new Product();
            
            try
            {
                modelJsAlpine = JsonSerializer.Deserialize<ModelJsAlpine<object>>(keyObjectProduct);
            }
             //Проверка modelJsAlpine на NULL;
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в сериализации, {ex.Message}");
            }
            if (modelJsAlpine == null)
            {
                return BadRequest("Json не удалось преобразовать.");
            }

            // заполнение модели;
            product.Name = modelJsAlpine.title;
            product.SkuNumber = modelJsAlpine.sku;
            product.Prise = modelJsAlpine.price;
            product.Lastprise = modelJsAlpine.oldPrice;
            product.Rating = (decimal)modelJsAlpine.rating;
            product.FullDiscription = modelJsAlpine.fullDesc;
            product.BriefDiscription = modelJsAlpine.shortDesc;
            if(Enum.TryParse(modelJsAlpine.status, out StatusProductEnum result))
            {
                product.Status = result;
            }

            // присвоение значение для объекта productMethod через промежуточный класс;
            #region
            if (!int.TryParse(modelJsAlpine.category, out int id))
                return BadRequest("Error. no category");
            var categoryFromBase = await _context.categorys.FindAsync(id); // Найден элемент
            if (categoryFromBase == null)
                return BadRequest("Error. No category");

            // Объект ProductCategory;
            var productCategory = new ProductCategory
            {
                Category = categoryFromBase,
                Product = product,
            };
            if (productCategory.Category.LevelCategory == 0)
            {
                product.IndicateCategory = productCategory.Category.Id;
            }
            #endregion

            // Добовление файлов картинок для Product;
            //(var f in imagesFiles)
            var mas = imagesFiles.ToList();

            for(int a=0; a<mas.Count;a++)
            {
                var namef = Guid.NewGuid() + Path.GetExtension(mas[a].FileName);
                var pathf = Path.Combine(_inv.WebRootPath, "FotoProduct", namef);
                using (var streemf = new FileStream(pathf, FileMode.Create))
                {
                    await mas[a].CopyToAsync(streemf);
                };
                var productpictureForDB = new ProductPicture();
                if (a == 0)
                {
                    productpictureForDB.MainPhoto = true;
                }
                productpictureForDB.Putch = "/FotoProduct/" + namef;
                productpictureForDB.NamePicture = namef;
                productpictureForDB.Product = product;
                await _context.productsPicture.AddAsync(productpictureForDB);
            }
            await _context.products.AddAsync(product);

            // Добовление Features для Product;
            foreach(string feat in modelJsAlpine.keyFeatures)
            {
                KeyFeature feature = new KeyFeature();
                feature.Name=feat;
                var productKeyFeatures = new ProductKeyFeature()
                {
                    KeyFeature = feature,
                    Product=product,
                };
                await _context.productKeyFeatures.AddAsync(productKeyFeatures);
                await _context.keyFeatures.AddAsync(feature);
            }

            // Добовление в BD Category;
            for(int a=0; a <modelJsAlpine.specs.Length;a++)
            {
                var specs = modelJsAlpine.specs[a];
                var features = specs.features;
                foreach(var t in features)
                {
                    var category = new Category()
                    {
                        LevelCategory = 1,
                        Name = t.key,
                        Description = t.value,
                        Title = specs.name
                    };
                    var productcategory = new ProductCategory()
                    {
                        Category = category,
                        Product = product
                    };
                    await _context.categorys.AddAsync(category);
                    await _context.productCategories.AddAsync(productcategory);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.productCategories.AddAsync(productCategory);
            _context.SaveChanges();
            return Ok();
        }

        // Метод, который достает из базы данныхм

    }
}
