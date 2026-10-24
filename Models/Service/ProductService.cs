using System.Collections.ObjectModel;
using System.Text.Json;
using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.Enams;
using Backend_asp.net.Models.Intermediate_class;
using Backend_asp.net.Models.Static_classes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace Backend_asp.net.Models.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repo;
        private readonly AplicationContext _contextServices;
        private readonly IWebHostEnvironment _inv;
        private readonly string _namefolder = "FotoProduct1"; //The name folder, where will be saved pictures;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IRepository repo, AplicationContext context, IWebHostEnvironment inv,
            ILogger<ProductService>logger )
        {
            _repo = repo;
            _contextServices = context;
            _inv = inv;
            _logger = logger;
        }

        //The method prepares the model for download page, where the catalog a product;
        public async Task<ProductsGeneral> ModelSendInViews()
        {
            var productGeneral = new ProductsGeneral();
            productGeneral.MainCategories = await _repo.GetMainCategoryFromDB();
            var listJsModel = new List<ModelJsAlpine<Product>>();
            var products = await _repo.GetAllProductList();
            foreach (var product in products)
            {
                MainCategory? maincategory = productGeneral.MainCategories.FirstOrDefault(t => t.Id == product.MainCategoryId);
                var jsmod = new ModelJsAlpine<Product>()
                {
                    id = product.Id,
                    title = product.Name,
                    sku = product.SkuNumber,
                    price = product.Prise,
                    oldPrice = product.Lastprise,
                    rating = (double)product.Rating,
                    shortDesc = product.BriefDiscription,
                    fullDesc = product.FullDiscription,
                    mainCategoryName = product.MainCategory?.Name,
                    statusInt = product.Status.ToString(),
                    status = StatusProduct.Name.First(t => t.Key == product.Status).Value,
                    createdAt = product.CreateRoverMechTime.ToString("dd/MM/yy/HH:mm"),
                    images = product.ProductPicturees.OrderByDescending(t=>t.MainPhoto)
                    .Select(t=>t.Putch).ToList()
                };
                if (product.MainCategory != null)
                    jsmod.mainCategory = product.MainCategory.Id;
                foreach (var keyfeat in product.ProductKeyFeatures)
                {
                    if (keyfeat.KeyFeature != null)
                    {
                        string keyfeature = keyfeat.KeyFeature.Name;
                        jsmod.keyFeatures.Add(keyfeature);
                    }
                }

                // RU: Добовление свойств товара для передачи в представлении;
                // Addit propertes the goods for transfing in views;
                var sortByCategory = product.PropertyValues.GroupBy(t => t.Category);
                foreach (var category in sortByCategory)
                {
                    var sp = new Specs();
                    foreach (var feat in category)
                    {
                        var feature =new Features()
                        {
                            key=feat.Property,
                            value= feat.Value
                        };
                        sp.name = feat.Category.Name;
                        sp.features.Add(feature);
                    }
                    jsmod.specs.Add(sp);
                }
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
        public async Task AddNewProduct(string Json, List<IFormFile> imagesFiles)
        {
            var modeljs = JsonSerialFromJS(Json);
            if (modeljs.id >= 0)
            {
                // The logics, where will be change a model;
                await ChengeModel(modeljs, imagesFiles);
                return;
            };
            var product = new Product()
            {   
                Name = modeljs.title,
                SkuNumber = modeljs.sku,
                Prise = modeljs.price,
                Lastprise = modeljs.oldPrice,
                BriefDiscription = modeljs.shortDesc,
                FullDiscription = modeljs.fullDesc,
                Rating = (decimal)modeljs.rating,
                MainCategoryId = modeljs.mainCategory
            };
            //filling the "ProductkeyFeature";
            foreach (string a in modeljs.keyFeatures)
            {
                var keyfeature = new KeyFeature()
                {
                    Name = a
                };
                var productkeyfeat = new ProductKeyFeature()
                {
                    KeyFeature = keyfeature,
                    Product = product
                };
                await _contextServices.KeyFeatures.AddAsync(keyfeature);
                await _contextServices.ProductKeyFeatures.AddAsync(productkeyfeat);
            }
            //filling the "Status"
            product.Status = StatusProduct.Name.First(t => t.Value == modeljs.status).Key;
            //TODO: RU: Добовляю свойства товара в базу данных;
            //filling the "categorys"
            if (modeljs.specs != null)
            {
                var categoryFromDB = await _contextServices.Categorys.ToListAsync();
                foreach (var spec in modeljs.specs)
                {
                    var category = categoryFromDB.FirstOrDefault(t => t.Name == spec.name);
                    ICollection<PropertyValue> properValue=new List<PropertyValue>();
                    if (category == null)
                    {
                        category = new Category()
                        {
                            Name = spec.name,
                        };
                        await _contextServices.Categorys.AddAsync(category);
                        if (spec.features != null)
                        {
                            properValue=AddStaticMethod.CreatePropertyValueFromFeatures(spec.features,category,product);
                        }
                    }
                    else
                        if(spec.features != null)
                        properValue = AddStaticMethod.CreatePropertyValueFromFeatures(spec.features, category, product);
                    foreach (var propertyValueSaveInDB in properValue)
                        _contextServices.PropertyValues.Add(propertyValueSaveInDB);
                    await _contextServices.SaveChangesAsync();
                }
            }
            // Adding files to the DB.
            var mainFile = true;
            foreach (var file in imagesFiles)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fileSaveAdressDirectory = Path.Combine(_inv.WebRootPath, _namefolder);
                if (!Directory.Exists(fileSaveAdressDirectory))
                    Directory.CreateDirectory(fileSaveAdressDirectory);
                var fullFileAdress = Path.Combine(fileSaveAdressDirectory, filename);
                using (var filestream = new FileStream(fullFileAdress, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                };
                var photo = new Photoproduct()
                {
                    NamePicture = filename,
                    Putch = $"/{_namefolder}/{filename}",
                    Product = product
                };
                if (mainFile == true)
                    photo.MainPhoto = true;
                mainFile = false;
                await _contextServices.Photoproducts.AddAsync(photo);
            }
            await _contextServices.SaveChangesAsync();
        }

        // RU: Метод, который будет изменять; 
        // The method for chenges a model on the page;
        public async Task ChengeModel(ModelJsAlpine<Object> jsmodel, List<IFormFile> imagesFiles)
        {   
            if(jsmodel.id == null)
            {
                _logger.LogError("RU: Ошибка в сериализации, не передан id товара для изменения.");
                Exception ex = new Exception("Ошибка в сериализации, не передан id товара для изменения.");
                throw ex;
            }
            var productDB=await _repo.GetProductById((int)jsmodel.id);
            // Chenging the model here;
            if (productDB != null)
            {
                productDB.Name = jsmodel.title;
                productDB.SkuNumber = jsmodel.sku;
                productDB.Prise = jsmodel.price;
                productDB.Lastprise = jsmodel.oldPrice;
                productDB.Time_of_chenge = DateTime.Now;
                productDB.Status = StatusProduct.Name.First(a => a.Value == jsmodel.status).Key;
                productDB.BriefDiscription = jsmodel.shortDesc;
                productDB.FullDiscription = jsmodel.fullDesc;
                productDB.Rating = (decimal)jsmodel.rating;
                productDB.MainCategoryId = jsmodel.mainCategory;
                // Changing the connectors many to many of tables ProductKeys feature and Key Feature;
                var keyfeaturesDB = productDB.ProductKeyFeatures.Select(t => t.KeyFeature?.Name).ToList();

                var keyfeatNew = jsmodel.keyFeatures.Except(keyfeaturesDB).ToList();
                var keyfeatDelete = keyfeaturesDB.Except(jsmodel.keyFeatures).ToList();

                if (keyfeatNew.Any() || keyfeatDelete.Any())
                {
                    // Removing associations for ProductKeyFeature;
                    foreach (var pkf in keyfeatDelete)
                    {
                        var prod = await _contextServices.ProductKeyFeatures
                            .FirstOrDefaultAsync(t => t.KeyFeature!.Name == pkf && productDB.Id == t.IdProduct);
                        if (prod != null)
                            _contextServices.ProductKeyFeatures.Remove(prod);
                    }
                    // Adding relations for ProductKeyFeature;
                    foreach (var prodkeyfeat in keyfeatNew)
                    {
                        // RU: проверяю есть ли совподение уже в BD
                        if (prodkeyfeat != null)
                        {
                            var keyfeatFromDB = await _contextServices.KeyFeatures
                                .FirstOrDefaultAsync(t => t.Name == prodkeyfeat);
                            if (keyfeatFromDB == null)
                            {
                                keyfeatFromDB = new KeyFeature { Name = prodkeyfeat };
                                _contextServices.KeyFeatures.Add(keyfeatFromDB);
                            }
                        }
                    }
                    await _contextServices.SaveChangesAsync();
                    foreach (var prodkeyfeat in keyfeatNew)
                    {
                        var keyfeatFromDB = await _contextServices.KeyFeatures
                                .FirstOrDefaultAsync(t => t.Name == prodkeyfeat);
                        if (keyfeatFromDB != null)
                        {
                            var addProdkeyfeat = new ProductKeyFeature
                            {
                                IdProduct = productDB.Id,
                                IdKeyFeature = keyfeatFromDB.Id
                            };
                            _contextServices.ProductKeyFeatures.Add(addProdkeyfeat);
                        }
                    }
                    await _contextServices.SaveChangesAsync();
                }

                // RU: проверка и замена кортинок;
                var picture = productDB.ProductPicturees.Select(pict => pict.Putch).ToList();
                var deletePictureFromDB = picture.Except(jsmodel.images).ToList();
                if (!jsmodel.images.SequenceEqual(picture) || imagesFiles != null)
                {
                    bool flag = true;   // Использование flag, чтоб один раз сработало условие ниже;
                    var pictures = productDB.ProductPicturees?.ToList();
                    int index = 0;
                    for (int a = 0; a < jsmodel.images.Count; a++)
                    {
                        if (pictures == null)
                            continue;
                        // RU: проверим одинаковый ли первый элемент, если да, то сразу меняем flag;
                        if (pictures[0].Putch == jsmodel.images[0])
                        {
                            flag = false;
                            continue;
                        }
                        // RU: если будет строка начинатся на "bloom" это значит пошли файлы, нужно сохронять в проект;
                        if (jsmodel.images[a].StartsWith("blob"))
                        {
                            // RU: Создаю поток файла;
                            var files = imagesFiles.ToList();
                            if (index < files.Count)
                            {
                                // RU: буду увеличивать по index;
                                // RU: Идет задвоение файлов, т.к. встречает два раза bloob и два раза гонит по массиву;
                                var fileName = Guid.NewGuid() + Path.GetExtension(files[index].FileName);
                                var fileAdresInServer = Path.Combine(_namefolder, fileName);
                                var fileFullAdress = Path.Combine(_inv.WebRootPath, fileAdresInServer);
                                using (var stream = new FileStream(fileFullAdress, FileMode.Create))
                                {
                                    await files[index].CopyToAsync(stream);
                                };
                                var photoproduct = new Photoproduct()
                                {
                                    NamePicture = fileName,
                                    Product = productDB,
                                    ProductId = productDB.Id,
                                    Putch = fileAdresInServer
                                };
                                if (flag)
                                {
                                    photoproduct.MainPhoto = true;
                                    flag = false;
                                }
                                await _contextServices.Photoproducts.AddAsync(photoproduct);
                                index++;
                            };
                        }

                        // TODO:
                        // RU: проверка имеется ли уже такие файлы и с какими они flags;
                        if (picture.Contains(jsmodel.images[a]))
                        {
                            foreach (var photoproduct in pictures)
                            {
                                if (photoproduct.Putch == jsmodel.images[a] && flag)
                                {
                                    var oldMain = pictures.FirstOrDefault(t => t.MainPhoto == true);
                                    if (oldMain != null)
                                        oldMain.MainPhoto = false;
                                    photoproduct.MainPhoto = true;
                                    await _contextServices.SaveChangesAsync();
                                    flag = false;
                                }
                            };
                        }
                    }
                    index = 0;
                    // RU: Удаляю файлы и записи в DB картинок, которые нужно удалить;
                    if (productDB.ProductPicturees != null)
                    {
                        foreach (var photoProduct in productDB.ProductPicturees)
                        {
                            if (deletePictureFromDB.Contains(photoProduct.Putch))
                            {
                                _contextServices.Photoproducts.Remove(photoProduct);
                                // Удаляем файлы из папки с этими именами;
                                var patch = Path.Combine(_inv.WebRootPath, photoProduct.Putch);
                                if (File.Exists(patch))
                                    File.Delete(patch);
                            };
                        };
                    }
                    await _contextServices.SaveChangesAsync();
                }

                // RU: Добовление свойств товара в базу данных;
                if (jsmodel.specs != null)
                {
                    var categoryFromDB = productDB.PropertyValues.ToList();
                    //RU: переборка уже из модели jsmodel.specs, которые пришли от клиента;
                    foreach (var spec in jsmodel.specs)
                    {
                        //RU: переборка категорий, которые пришли от клиента: List<Features> features;
                        foreach ( var feat in spec.features)
                        {
                            bool categoryExistinDB= categoryFromDB.Any(t => t.Property == feat.key && t.Value == feat.value);
                            {
                                if (!categoryExistinDB)
                                {
                                    var property = new PropertyValue()
                                    {
                                        Property = feat.key,
                                        Value = feat.value,
                                        Product= productDB
                                    };
                                    var categorysFromDBAsync = await _repo.GetCategorysFromDB();
                                    var categoryResultFound = categorysFromDBAsync.FirstOrDefault(t => t.Name == spec.name);
                                    if (categoryResultFound != null)
                                    {
                                        property.Category= categoryResultFound;
                                        _contextServices.PropertyValues.Add(property);
                                    }
                                    else
                                    {
                                        var categoryWriteDB = new Category()
                                        {
                                            Name = spec.name
                                        };
                                        categoryWriteDB.PropertyValues.Add(property);
                                        _contextServices.Categorys.Add(categoryWriteDB);
                                        await _contextServices.SaveChangesAsync();
                                    }

                                };
                            };
                        };
                    };
                    //RU: переборка категорий, которые уже есть в базе данных, если их нету в модели jsmodel.specs, то нужно удалить;
                    foreach (var categoryDB in categoryFromDB)
                    {
                        bool categoryExistinModel = jsmodel.specs.Any(t => t.name == categoryDB.Category?.Name && t.features.Any(f => f.key == categoryDB.Property && f.value == categoryDB.Value));
                        if (!categoryExistinModel)
                        {
                            _contextServices.PropertyValues.Remove(categoryDB);
                        }
                    }
                    await _contextServices.SaveChangesAsync();
                }
            }
            else
            {
                _logger.LogInformation("RU: Не найден товар в базе данных для изменения.");
            }
        }

        // Remove product from DB;
        public async Task ProductDelete(int id)
        {
            var product = new Product();
            product = await _contextServices.Products
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(feat => feat.KeyFeature)
                .Include(pict => pict.ProductPicturees)
                .Include(maincat => maincat.MainCategory)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (product == null)
                return;
            //TEMP:I'll delete it later;
            var keyfeats = product.ProductKeyFeatures.Select(t => t.KeyFeature).ToList();
            foreach (var key in keyfeats)
            {
                if (key != null)
                    _contextServices.KeyFeatures.Remove(key);
            }
            // The delete phisical files;
            var pictures = product.ProductPicturees.ToList();
            foreach (var picture in pictures)
            {
                var patch = Path.Combine(_inv.WebRootPath, _namefolder, picture.NamePicture);
                Console.WriteLine($"Путь к файлу:{patch}");
                if (System.IO.File.Exists(patch))
                    System.IO.File.Delete(patch);
            }
            _contextServices.Products.Remove(product);
            await _contextServices.SaveChangesAsync();
        }

        // RU: Метод который передает, объект из базы данные по id;
        public async Task<ProductsGeneral> GetProductById(int id)
        {
            var productGeneral = new ProductsGeneral();
            var productFromDB = await _repo.GetProductById(id);

            //RU: подготовка модели Product() для того, чтоб можно было передать в карточку продукта; 
            productGeneral.ProductFromAdmin=productFromDB;
            productGeneral.ProductFromAdmin.Images = productFromDB.ProductPicturees
                .OrderByDescending(a => a.MainPhoto == true)
                .Select(a => a.Putch).ToList();

            //RU: делаю готовый путь для передачи картинок;
            var listNormalPath = new List<string>();
            foreach (var normalPath in productGeneral.ProductFromAdmin.Images)
            {
                var fixpatch = normalPath.Replace("\\", "/");
                fixpatch = "/" + fixpatch;
                listNormalPath.Add(fixpatch);
            }
            productGeneral.ProductFromAdmin.Images = listNormalPath;
            var Specs = productFromDB.PropertyValues.GroupBy(t => t.Category)
                .Select(spec => new Specs
                {
                    name = spec.Key.Name,
                    features = spec.Select(feat => new Features
                    {
                        key = feat.Property,
                        value = feat.Value
                    }).ToList()
                }).ToList();
            productGeneral.Specs = Specs;
            return productGeneral;
        }
    }
}




