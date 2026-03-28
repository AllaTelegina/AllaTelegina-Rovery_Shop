using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            ModelJsAlpine<Product> modelJsAlpine = new ModelJsAlpine<Product>();

            // Собираю модель из BD;
            var products = await _context.products
                .Include(t => t.ProductCategoryes)
                .Include(t => t.ProductPicturees)
                .Include(t => t.ProductKeyFeatures)
                .ToListAsync();
            DotnetModel dotnetModel = new DotnetModel();
            dotnetModel.model_products = products;
            return View("katalog", dotnetModel);
        }
    }   
}
