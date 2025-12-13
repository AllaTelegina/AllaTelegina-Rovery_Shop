using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // Открытие базы данных, определение context;
        private readonly AplicationContext _context;
        // Конструктор для создания контекста;
        public ProductController(AplicationContext context)
        {
            _context = context;
        }

        //---------------------------------<< Методы контроллера  >>------------------------------------//
        // Для загрузки этого представления;
        public async Task <IActionResult> ProductsView()
        {
            var product = new ProductsGeneral();
            product.ProductFromAdmin = new Product();
            product.ListCategorys=await _context.categorys.ToListAsync();
            return View("~/Areas/Admin/Views/Products/index.cshtml", product);
        }

        // Добовления товара;
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
            product.IsCategory = categoryFromIsCategory.Id;
            return RedirectToAction("ProductsView");
        }

        // Метод, который будет вызван при отправки из запроса .js
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
                case "электровелосипеды":
                {
                    // электровелосипеды -      "2000000"
                    var skuname = 0;
                    Product product = await _context.products.LastAsync();
                    if (int.TryParse(product.SkuNumber, out int x))
                    {
                        skuname = x;
                    }
                    product.SkuNumber = (skuname++).ToString();
                    return Ok(product);
                    // Добавить условие при котором, если не получится Parse числа; #дописать;
                }
            }
            return Ok();
        }
    }
}
