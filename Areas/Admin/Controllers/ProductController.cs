using Backend_asp.net.DataBase;
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
            product.ListCategorys=await _context.categorys.ToListAsync();
            return View("~/Areas/Admin/Views/Products/index.cshtml", product);
        }
    }
}
