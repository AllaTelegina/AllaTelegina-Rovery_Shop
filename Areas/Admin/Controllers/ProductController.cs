using System.ClientModel.Primitives;
using System.Text.Json;
using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Enams;
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
            // TODO: передаю список моделей;
            var products = await _context.products
                .Include(t => t.ProductCategoryes)
                .ThenInclude(a => a.Category)
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(a => a.KeyFeature)
                .Include(t => t.ProductPicturees)
                .ToListAsync();
            if (products == null)
                throw new Exception("Товары не найдены в базе данных");
            // Список объектов, которые я собираю для передачи в модель, которая для представления;
            List<ModelJsAlpine<Product>> send_modelJsAlpines = new List<ModelJsAlpine<Product>>();

            // Для проверки сборки всех свойств:
            var test_All_Specs = new List<Specs>();

            foreach (var prod in products)
            {
                var modjs = new ModelJsAlpine<Product>()
                {
                    id = prod.Id,
                    title = prod.Name,
                    sku = prod.SkuNumber,
                    price = prod.Prise,
                    oldPrice = prod.Lastprise,
                    rating = (double)prod.Rating,
                    status = StatusProduct.Name[prod.Status],
                    shortDesc = prod.BriefDiscription,
                    fullDesc = prod.FullDiscription,
                };

                // Присвоение главной категории для модели;
                foreach (var category in prod.ProductCategoryes)
                {
                    if (category != null)
                    {
                        if (category.Category == null)
                            throw new Exception("Не найдена главная категория");
                        if (category.Category.LevelCategory == 0)
                        {
                            modjs.category = category.Category.Name;
                            modjs.categoryId=category.Category.Id;
                        }
                    }
                }

                // фото;
                if (prod.ProductPicturees.Any())
                {
                    //var pict = prod.ProductPicturees.ElementAt(0);
                    //modjs.pictures = new string[] { pict.Putch };
                    modjs.pictures = new string[prod.ProductPicturees.Count];
                    var listArr = new List<string>();
                    foreach (var foto in prod.ProductPicturees)
                    {
                        listArr.Add(foto.Putch);
                    }
                    // Копирую массив картинок в массив модели;
                    modjs.pictures = listArr.ToArray();
                }

                // Дата создания;
                modjs.Create_time = prod.CreateRoverMechTime.ToString("dd.MM.yyyy (HH:mm)");

                // Features;
                ICollection<string> listfeat = new List<string>();
                foreach (var feat in prod.ProductKeyFeatures)
                {
                    if (feat.KeyFeature != null)
                        listfeat.Add(feat.KeyFeature.Name);
                }
                modjs.keyFeatures = listfeat.ToArray();

                // Дата изменения:
                if (prod.Time_of_chenge==null)
                    modjs.Time_of_chenge = " ";
                else
                {
                    modjs.Time_of_chenge = prod.Time_of_chenge.Value.ToString("dd.MM.yyyy (HH:mm)");
                }

                // TEMP: присвоение для модели свойств;
                // Передача свойств товара;
                var cateory_in_List = prod.ProductCategoryes.ToList();     //собрал весь список категорий;
                var listSpecs = new List<Specs>();


                for(int i=0; i<cateory_in_List.Count();)        // перебираю его
                {
                    var specs = new Specs();
                    var listFeatures = new List<Features>();
                    var next = cateory_in_List.ElementAtOrDefault(i+1);
                    {
                        if (cateory_in_List[i]?.Category?.LevelCategory > 0)    // отсеиваю главные категории из этого списка
                        {
                            do
                            {
                                if (next != null)
                                {
                                    specs.name = cateory_in_List[i].Category.Title;
                                    var feat = new Features()
                                    {
                                        key = cateory_in_List[i].Category.Name,
                                        value = cateory_in_List[i].Category.Description
                                    };
                                    listFeatures.Add(feat);
                                    i++;
                                    next = cateory_in_List.ElementAtOrDefault(i);
                                }
                            }
                            while (next != null && specs.name == cateory_in_List[i].Category.Title);
                            if (next == null)
                                i++;
                            specs.features = listFeatures.ToArray();
                            listSpecs.Add(specs);
                        }
                        else
                            i++;
                    }
                }
                send_modelJsAlpines.Add(modjs);
            };




            product.modelJsAlpines = send_modelJsAlpines;
            return View("~/Areas/Admin/Views/Products/index.cshtml", product);
        }
        #endregion

        // Добовления товара в BD;
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

        // Метод, который будет вызван при отправки из запроса .js, который присвоит SKU номер автоматически
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
                        var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
                        if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 1000000 && x < 2000000)
                        {
                            skuname = x;
                            skuname++;
                            // Добавить условие при котором, если не получится Parse числа; #дописать;
                            product.SkuNumber = (skuname).ToString();
                            return Ok(product);
                        }
                        else
                        {
                            product = new Product();
                            product.SkuNumber = 1000001.ToString();
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
                        if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 2000000 && x < 3000000)
                        {
                            skuname = x;
                            skuname++;
                            // Добавить условие при котором, если не получится Parse числа; #дописать;
                            product.SkuNumber = (skuname).ToString();
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
                        if (product != null && int.TryParse(product.SkuNumber, out int x) && x>= 3000000 && x< 4000000)
                        {
                            skuname = x;
                            skuname++;
                            product.SkuNumber = (skuname).ToString();
                            return Ok(product);
                        }
                        // Добавить условие при котором, если не получится Parse числа; #дописать;
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
                        if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 4000000 && x < 5000000)
                        {
                            skuname = x;
                            skuname++;
                            // Добавить условие при котором, если не получится Parse числа; #дописать;
                            product.SkuNumber = (skuname).ToString();
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
                        if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 5000000 && x < 6000000)
                        { 
                            skuname = x;
                            skuname++;
                            // Добавить условие при котором, если не получится Parse числа; #дописать;
                            product.SkuNumber = (skuname).ToString();
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

        // Метод для добовления товара черз API JS в BD;
        #region
        [HttpPost]
        public async Task <IActionResult> AddProductJS( [FromForm] string keyObjectProduct, [FromForm] List<IFormFile> imagesFiles)
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
            // Условие для редактирования модели;
            if (modelJsAlpine.id > 0)
            {
                await AditionModel(modelJsAlpine, imagesFiles);
                return Ok();
            }

            // заполнение модели для добовления в базу данных;
            #region
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
            // присвоение в product category;
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
            // Добовление файлов картинок для Product;
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
            foreach (string feat in modelJsAlpine.keyFeatures)
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
            #endregion
            return Ok();
        }
        #endregion

        // Метод для перезагрузки представления;
        private IActionResult ApdateProductController()
        {
            return RedirectToAction("ProductsView");
        }

        // Метод для редактирования модели;
        private async Task <IActionResult> AditionModel(ModelJsAlpine<object> modelJsAlpine, List<IFormFile> imagesFiles)
        {
            var product=await _context.products
                .Include(t => t.ProductCategoryes)
                .ThenInclude(a => a.Category)
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(a => a.KeyFeature)
                .Include(t => t.ProductPicturees)
                .FirstOrDefaultAsync(t=>t.Id==modelJsAlpine.id);
            if (product == null)
            {
                return BadRequest("Error. No product");
            }
            product.Name = modelJsAlpine.title;
            product.SkuNumber = modelJsAlpine.sku;
            product.Prise = modelJsAlpine.price;
            product.Lastprise = modelJsAlpine.oldPrice;
            product.Rating = (decimal)modelJsAlpine.rating;
            product.FullDiscription = modelJsAlpine.fullDesc;
            product.BriefDiscription = modelJsAlpine.shortDesc;
            product.Time_of_chenge = DateTime.Now;

            // редактирование статуса заказа;
            if (Enum.TryParse(modelJsAlpine.status, out StatusProductEnum result))
            {
                product.Status = result;
            }

            // редактирование главной категории в Category, где главная категория имеет LevelCategory=0;
            var productCategory_for_write = new ProductCategory();
            foreach (var category in product.ProductCategoryes)
            {
                if (category.Category == null)
                    throw new Exception("Главная категория не имеет значения");
                if (category.Category.LevelCategory == 0)
                {
                    _context.productCategories.Remove(category);
                }
            }
            productCategory_for_write.CategoryId = modelJsAlpine.categoryId;
            productCategory_for_write.ProductId = modelJsAlpine.id;
            await _context.productCategories.AddAsync(productCategory_for_write);

            // редактирования фото товаров;
            ICollection < ProductPicture > productPicture= new List<ProductPicture>();
            bool flag = true;   // Использование flag, чтоб один раз сработало условие ниже;
            for(int i = 0; i < modelJsAlpine.images.Count(); i++)
            {
                // Условие при котором если строка будет из "blob" то запишим файлы в коллекцию productPicture;
                if (flag==true && modelJsAlpine.images[i].StartsWith("blob:"))
                {
                    // Открываю поток и копирую файл в свою директорию, и этому пути файлу придаю сразу значение MainPhoto для этого пути;
                    flag = false; 
                    var mas = imagesFiles.ToList();
                    for (int a = 0; a < mas.Count; a++)
                    {
                        var namef = Guid.NewGuid() + Path.GetExtension(mas[a].FileName);
                        var pathf = Path.Combine(_inv.WebRootPath, "FotoProduct", namef);
                        using (var streemf = new FileStream(pathf, FileMode.Create))
                        {
                            await mas[a].CopyToAsync(streemf);
                        };
                        ProductPicture pict = new ProductPicture
                        {
                            NamePicture = namef,
                            Putch= "/FotoProduct/" + namef,
                            ProductId =product.Id,
                            Product=product
                        };
                        if (i == 0 && a==0)
                        {
                            pict.MainPhoto = true;
                        }
                        productPicture.Add(pict);
                    }
                }
                else
                {
                    if (!modelJsAlpine.images[i].StartsWith("blob:"))
                    {
                        var picture = new ProductPicture
                        {
                            Product= product,
                            ProductId=product.Id,
                            Putch= modelJsAlpine.images[i]
                        };
                        if (i == 0)
                            picture.MainPhoto = true;
                        picture.NamePicture = modelJsAlpine.images[i].Split("/").Last();
                        productPicture.Add(picture);
                    }
                }
            }
            product.ProductPicturees.Clear();
            foreach (var pict in productPicture)
                product.ProductPicturees.Add(pict);

            // Редактирование KeyFeature;
            ICollection<ProductKeyFeature> db_productkeyfet = product.ProductKeyFeatures.ToList();
            foreach (var keyfeat in db_productkeyfet)
            {
                _context.productKeyFeatures.Remove(keyfeat);
                await _context.SaveChangesAsync();
            }
            for(int a=0; a<modelJsAlpine.keyFeatures.Count(); a++)
            {
                var newkeyfeat = new KeyFeature()
                {
                    Name = modelJsAlpine.keyFeatures[a]
                };
                var prodkeyfeat = new ProductKeyFeature()
                {
                    IdKeyFeature=newkeyfeat.Id,
                    IdProduct=modelJsAlpine.id,
                    KeyFeature=newkeyfeat
                };
                _context.productKeyFeatures.Add(prodkeyfeat);
                await _context.SaveChangesAsync();
            };
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
