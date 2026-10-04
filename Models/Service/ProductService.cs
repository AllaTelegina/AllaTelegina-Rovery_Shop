using System.Collections.ObjectModel;
using System.Text.Json;
using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Enams;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Backend_asp.net.Models.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repo;
        private readonly AplicationContext _contextServices;
        private readonly IWebHostEnvironment _inv;
        private readonly string _namefolder = "FotoProduct1"; //The name folder, where will be saved pictures;
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
                await _contextServices.keyFeatures.AddAsync(keyfeature);
                await _contextServices.productKeyFeatures.AddAsync(productkeyfeat);
            }
            //filling the "Status"
            product.Status = StatusProduct.Name.First(t => t.Value == modeljs.status).Key;
            //filling the "categorys"
            foreach (var category in modeljs.specs)
            {
                var categorProduct = new Category()
                {
                    Name = category.name,
                };
                foreach (var propertyType in category.features)
                {
                    var propertyTypeTable = new PropertyType()
                    {
                        Name = propertyType.key
                    };
                    var propertyTypeValue = new PropertyTypeValue()
                    {
                        Value = propertyType.value,
                        PropertyType = propertyTypeTable,
                        Category = categorProduct
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
                await _contextServices.productsPicture.AddAsync(photo);
            }
            await _contextServices.SaveChangesAsync();
        }

        // The method for chenges a model on the page;
        public async Task ChengeModel(ModelJsAlpine<Object> jsmodel, List<IFormFile> imagesFiles)
        {
            var productDB = await _contextServices.products
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(feat => feat.KeyFeature)
                .Include(pict => pict.ProductPicturees)
                .Include(maincat => maincat.MainCategory)
                .Include(prodcat => prodcat.ProductCategoryes)
                .ThenInclude(cat => cat.Category)
                .ThenInclude(proptypeval => proptypeval!.PropertyTypeValues)
                .ThenInclude(proptype => proptype.PropertyType)
                .FirstOrDefaultAsync(t => t.Id == jsmodel.id);
            if (productDB == null)
                return;
            // Chenging the model here;
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
                    var prod = await _contextServices.productKeyFeatures
                        .FirstOrDefaultAsync(t => t.KeyFeature!.Name == pkf && productDB.Id == t.IdProduct);
                    if (prod != null)
                        _contextServices.productKeyFeatures.Remove(prod);
                }
                // Adding relations for ProductKeyFeature;
                foreach (var prodkeyfeat in keyfeatNew)
                {
                    // RU: проверяю есть ли совподение уже в BD
                    if (prodkeyfeat != null)
                    {
                        var keyfeatFromDB = await _contextServices.keyFeatures
                            .FirstOrDefaultAsync(t => t.Name == prodkeyfeat);
                        if (keyfeatFromDB == null)
                        {
                            keyfeatFromDB = new KeyFeature { Name = prodkeyfeat };
                            _contextServices.keyFeatures.Add(keyfeatFromDB);
                        }
                    }
                }
                await _contextServices.SaveChangesAsync();
                foreach (var prodkeyfeat in keyfeatNew)
                {
                    var keyfeatFromDB = await _contextServices.keyFeatures
                            .FirstOrDefaultAsync(t => t.Name == prodkeyfeat);
                    if (keyfeatFromDB != null)
                    {
                        var addProdkeyfeat = new ProductKeyFeature
                        {
                            IdProduct = productDB.Id,
                            IdKeyFeature = keyfeatFromDB.Id
                        };
                        _contextServices.productKeyFeatures.Add(addProdkeyfeat);
                    }
                }
                await _contextServices.SaveChangesAsync();
            }

            // RU: проверка и замена кортинок;
            var picture = productDB.ProductPicturees.Select(pict => pict.Putch).ToList();
            // RU: проверка пришли ли вообще изминения;
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
                            // TODO: RU: Идет задвоение файлов, т.к. встречает два раза bloob и два раза гонит по массиву;
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
                            await _contextServices.productsPicture.AddAsync(photoproduct);
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
                            _contextServices.productsPicture.Remove(photoProduct);
                            // Удаляем файлы из папки с этими именами;
                            var patch = Path.Combine(_inv.WebRootPath, photoProduct.Putch);
                            if (File.Exists(patch))
                                File.Delete(patch);
                        };
                    };
                }
                await _contextServices.SaveChangesAsync();
            }

            // TODO:
            // RU: Проверка и замена свойст товара (проверку делаю ручно);
            if(jsmodel.specs!=null && productDB.ProductCategoryes!=null)
            {
                //RU: собираю похожую модель на ModelJsAlpine  из DB  для сравнения;
                var specAll = new List<Specs>();
                foreach(var prodCat in productDB.ProductCategoryes)
                {
                    var spec = new Specs();
                    if (prodCat.Category != null)
                    {
                        spec.name = prodCat.Category.Name;
                        //RU: тут же создаю модель Features();
                        if (prodCat.Category != null)
                        {
                            foreach(var catdb in prodCat.Category.PropertyTypeValues)
                            {
                                var modelSpec = new Features();
                                modelSpec.key = catdb.PropertyType.Name;
                                modelSpec.value = catdb.Value;
                                spec.features.Add(modelSpec);
                            }
                        }
                    }
                    specAll.Add(spec);
                }
                if (specAll == jsmodel.specs)
                    Console.WriteLine("модель равна данной");
                else
                    Console.WriteLine("модель не равна, но на самом деле равна ищи другой способ для логики");
            }
        }

        // Remove product from DB;
        public async Task ProductDelete(int id)
        {
            var product = new Product();
            product = await _contextServices.products
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(feat => feat.KeyFeature)
                .Include(pict => pict.ProductPicturees)
                .Include(maincat => maincat.MainCategory)
                .Include(prodcat => prodcat.ProductCategoryes)
                .ThenInclude(cat => cat.Category)
                .ThenInclude(proptypeval => proptypeval!.PropertyTypeValues)
                .ThenInclude(proptype => proptype.PropertyType)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (product == null)
                return;
            //TEMP:I'll delete it later;
            var keyfeats = product.ProductKeyFeatures.Select(t => t.KeyFeature).ToList();
            foreach (var key in keyfeats)
            {
                if (key != null)
                    _contextServices.keyFeatures.Remove(key);
            }
            var categorys = product.ProductCategoryes.Select(t => t.Category).ToList();
            foreach (var category in categorys)
            {
                if (category != null)
                    _contextServices.categorys.Remove(category);
            }
            var propertyTypes = categorys.Where(t => t!.PropertyTypeValues != null).SelectMany(t => t!.PropertyTypeValues)
                .Select(a => a.PropertyType).Distinct().ToList();
            foreach (var propertytype in propertyTypes)
                _contextServices.PropertyTypes.Remove(propertytype);
            //
            // The delete phisical files;
            var pictures = product.ProductPicturees.ToList();
            foreach (var picture in pictures)
            {
                var patch = Path.Combine(_inv.WebRootPath, _namefolder, picture.NamePicture);
                Console.WriteLine($"Путь к файлу:{patch}");
                if (System.IO.File.Exists(patch))
                    System.IO.File.Delete(patch);
            }
            _contextServices.products.Remove(product);
            await _contextServices.SaveChangesAsync();
        }
    }
}
