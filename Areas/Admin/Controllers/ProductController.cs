using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text.Json;
using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Enams;
using Backend_asp.net.Models.Intermediate_class;
using Backend_asp.net.Models.Repository;

//using Backend_asp.net.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _inv;
        private readonly IRepository _repos;
        private readonly IProductService _prodServ;

        // Create new constructor;
        public ProductController(IRepository repos, IWebHostEnvironment inv, IProductService prodServ)
        {
            _repos = repos;
            _inv = inv;
            _prodServ = prodServ;
        }

        //---------------------------------<< Методы контроллера  >>------------------------------------//
        // Start the page of catalog
        public async Task<IActionResult> ProductsView()
        {
            var result = await _prodServ.ModelSendInViews();
            return View("~/Areas/Admin/Views/Products/index.cshtml", result);
        }

        // Adding product in Data Base;
        [HttpPost]
        public async Task <IActionResult> AddProductJS( [FromForm] string keyObjectProduct, [FromForm] List<IFormFile> imagesFiles)
        {
            await _prodServ.AddNewProduct(keyObjectProduct, imagesFiles);
            return Ok();
        }

        // TEMP: разобраться с этим методом, не понятно, что он делает. Добовления товара в BD ;
        #region
        //[HttpPost]
        //public async Task<IActionResult> SaveProductForAdmin(ProductsGeneral productparameter, int IsCategory)
        //{
        //    if(productparameter==null || productparameter.ProductFromAdmin==null)
        //        return BadRequest("Send error data.");
        //    var product = productparameter.ProductFromAdmin;
        //    // Для объекта product определим Category IsCategory;
        //    var categoryFromIsCategory = await _context.categorys.FirstOrDefaultAsync(a => a.Id==IsCategory);
        //    if (categoryFromIsCategory == null)
        //        return NotFound($"Категория с Id {IsCategory} не найдена");
        //    // Добовление в базу данных главной категории;
        //    #region
        //    ProductCategory productCategory = new();
        //    productCategory.CategoryId= categoryFromIsCategory.Id;
        //    product.ProductCategoryes.Add(productCategory);
        //    #endregion

        //    return RedirectToAction("ProductsView");
        //}
        #endregion

        // Метод, который будет вызван при отправки из запроса .js, который присвоит SKU номер автоматически
        #region
        // Для формирования и присвоения для продукта SKU;
        //[HttpGet("SendSKU/{id}")]
        //public async Task<IActionResult> SendSKU(int id)
        //{
        //    var category= await _context.categorys.FirstOrDefaultAsync(c => c.Id==id);
        //    if (category == null)
        //        return NotFound("Not found category.");
        //    switch (category.Name)
        //    {
        //        case "Велосипеды":
        //        {
        //            try
        //            {
        //                // пример для велосипеды -             "1000000"
        //                var skuname = 0;
        //                var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
        //                if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 1000000 && x < 2000000)
        //                {
        //                    skuname = x;
        //                    skuname++;
        //                    // Добавить условие при котором, если не получится Parse числа; #дописать;
        //                    product.SkuNumber = (skuname).ToString();
        //                    return Ok(product);
        //                }
        //                else
        //                {
        //                    product = new Product();
        //                    product.SkuNumber = 1000001.ToString();
        //                    return Ok(product);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Ошибка выполнения \n" + ex);
        //                break;
        //            }
        //        }
        //        case "Электровелосипеды":
        //        {
        //            try
        //            {
        //                // пример для электровелосипедов -             "2000000"
        //                var skuname = 0;
        //                var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
        //                if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 2000000 && x < 3000000)
        //                {
        //                    skuname = x;
        //                    skuname++;
        //                    // Добавить условие при котором, если не получится Parse числа; #дописать;
        //                    product.SkuNumber = (skuname).ToString();
        //                    return Ok(product);
        //                }
        //                else
        //                {
        //                    product = new Product();
        //                    product.SkuNumber = 2000001.ToString();
        //                    return Ok(product);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Ошибка выполнения \n" + ex);
        //                break;
        //            }
        //        }
        //        case "Cамокаты":
        //        {
        //            try
        //            {
        //                // пример для самокаты -             "3000000"
        //                var skuname = 0;
        //                var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
        //                if (product != null && int.TryParse(product.SkuNumber, out int x) && x>= 3000000 && x< 4000000)
        //                {
        //                    skuname = x;
        //                    skuname++;
        //                    product.SkuNumber = (skuname).ToString();
        //                    return Ok(product);
        //                }
        //                // Добавить условие при котором, если не получится Parse числа; #дописать;
        //                else
        //                {
        //                    product = new Product();
        //                    product.SkuNumber = 3000001.ToString();
        //                    return Ok(product);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Ошибка выполнения \n" + ex);
        //                break;
        //            }
        //        }
        //        case "Электросамокаты":
        //        {
        //            try
        //            {
        //                // пример для электросамокаты -             "4000000"
        //                var skuname = 0;
        //                var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
        //                if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 4000000 && x < 5000000)
        //                {
        //                    skuname = x;
        //                    skuname++;
        //                    // Добавить условие при котором, если не получится Parse числа; #дописать;
        //                    product.SkuNumber = (skuname).ToString();
        //                    return Ok(product);
        //                }
        //                else
        //                {
        //                    product = new Product();
        //                    product.SkuNumber = 4000001.ToString();
        //                    return Ok(product);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Ошибка выполнения \n" + ex);
        //                break;
        //            }
        //        }
        //        case "Аксессуары":
        //        {
        //            try
        //            {
        //                // пример для аксессуары -             "5000000"
        //                var skuname = 0;
        //                var product = await _context.products.OrderBy(x => x.SkuNumber).LastOrDefaultAsync();
        //                if (product != null && int.TryParse(product.SkuNumber, out int x) && x >= 5000000 && x < 6000000)
        //                { 
        //                    skuname = x;
        //                    skuname++;
        //                    // Добавить условие при котором, если не получится Parse числа; #дописать;
        //                    product.SkuNumber = (skuname).ToString();
        //                    return Ok(product);
        //                }
        //                else
        //                {
        //                    product = new Product();
        //                    product.SkuNumber = 5000001.ToString();
        //                    return Ok(product);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Ошибка выполнения \n" + ex);
        //                break;
        //            }
        //        }
        //    }
        //    return Ok();
        //}
        #endregion


        // Метод для перезагрузки представления;
        private IActionResult ApdateProductController()
        {
            return RedirectToAction("ProductsView");
        }

        //Метод для редактирования модели;
        private async Task<IActionResult> AditionModel(ModelJsAlpine<object> modelJsAlpine, List<IFormFile> imagesFiles)
        {
            await _prodServ.ChengeModel(modelJsAlpine, imagesFiles);
            //    var product=await _context.products
            //        .Include(t => t.ProductCategoryes)
            //        .ThenInclude(a => a.Category)
            //        .Include(t => t.ProductKeyFeatures)
            //        .ThenInclude(a => a.KeyFeature)
            //        .Include(t => t.ProductPicturees)
            //        .FirstOrDefaultAsync(t=>t.Id==modelJsAlpine.id);
            //    if (product == null)
            //    {
            //        return BadRequest("Error. No product");
            //    }
            //    product.Name = modelJsAlpine.title;
            //    product.SkuNumber = modelJsAlpine.sku;
            //    product.Prise = modelJsAlpine.price;
            //    product.Lastprise = modelJsAlpine.oldPrice;
            //    product.Rating = (decimal)modelJsAlpine.rating;
            //    product.FullDiscription = modelJsAlpine.fullDesc;
            //    product.BriefDiscription = modelJsAlpine.shortDesc;
            //    product.Time_of_chenge = DateTime.Now;

            //    // редактирование статуса заказа;
            //    if (Enum.TryParse(modelJsAlpine.status, out StatusProductEnum result))
            //    {
            //        product.Status = result;
            //    }

            //    // редактирование главной категории в Category, где главная категория имеет LevelCategory=0;
            //    var productCategory_for_write = new ProductCategory();
            //    foreach (var category in product.ProductCategoryes)
            //    {
            //        if (category.Category == null)
            //            throw new Exception("Главная категория не имеет значения");
            //        if (category.Category.LevelCategory == 0)
            //        {
            //            _context.productCategories.Remove(category);
            //        }
            //    }
            //    productCategory_for_write.CategoryId = modelJsAlpine.categoryId;
            //    productCategory_for_write.ProductId = modelJsAlpine.id;
            //    await _context.productCategories.AddAsync(productCategory_for_write);

            //    // редактирования фото товаров;
            //    ICollection < ProductPicture > productPicture= new List<ProductPicture>();
            //    bool flag = true;   // Использование flag, чтоб один раз сработало условие ниже;
            //    for(int i = 0; i < modelJsAlpine.images.Count(); i++)
            //    {
            //        // Условие при котором если строка будет из "blob" то запишим файлы в коллекцию productPicture;
            //        if (flag==true && modelJsAlpine.images[i].StartsWith("blob:"))
            //        {
            //            // Открываю поток и копирую файл в свою директорию, и этому пути файлу придаю сразу значение MainPhoto для этого пути;
            //            flag = false; 
            //            var mas = imagesFiles.ToList();
            //            for (int a = 0; a < mas.Count; a++)
            //            {
            //                var namef = Guid.NewGuid() + Path.GetExtension(mas[a].FileName);
            //                var pathf = Path.Combine(_inv.WebRootPath, "FotoProduct", namef);
            //                using (var streemf = new FileStream(pathf, FileMode.Create))
            //                {
            //                    await mas[a].CopyToAsync(streemf);
            //                };
            //                ProductPicture pict = new ProductPicture
            //                {
            //                    NamePicture = namef,
            //                    Putch= "/FotoProduct/" + namef,
            //                    ProductId =product.Id,
            //                    Product=product
            //                };
            //                if (i == 0 && a==0)
            //                {
            //                    pict.MainPhoto = true;
            //                }
            //                productPicture.Add(pict);
            //            }
            //        }
            //        else
            //        {
            //            if (!modelJsAlpine.images[i].StartsWith("blob:"))
            //            {
            //                var picture = new ProductPicture
            //                {
            //                    Product= product,
            //                    ProductId=product.Id,
            //                    Putch= modelJsAlpine.images[i]
            //                };
            //                if (i == 0)
            //                    picture.MainPhoto = true;
            //                picture.NamePicture = modelJsAlpine.images[i].Split("/").Last();
            //                productPicture.Add(picture);
            //            }
            //        }
            //    }
            //    product.ProductPicturees.Clear();
            //    foreach (var pict in productPicture)
            //        product.ProductPicturees.Add(pict);

            // Редактирование KeyFeature;
            //        ICollection<ProductKeyFeature> db_productkeyfet = product.ProductKeyFeatures.ToList();
            //        foreach (var keyfeat in db_productkeyfet)
            //        {
            //            _context.productKeyFeatures.Remove(keyfeat);
            //            await _context.SaveChangesAsync();
            //        }
            //        for(int a=0; a<modelJsAlpine.keyFeatures.Count(); a++)
            //        {
            //            var newkeyfeat = new KeyFeature()
            //            {
            //                Name = modelJsAlpine.keyFeatures[a]
            //            };
            //            var prodkeyfeat = new ProductKeyFeature()
            //            {
            //                IdKeyFeature=newkeyfeat.Id,
            //                IdProduct=modelJsAlpine.id,
            //                KeyFeature=newkeyfeat
            //            };
            //            _context.productKeyFeatures.Add(prodkeyfeat);
            //            await _context.SaveChangesAsync();
            //        };
            //        await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
