using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
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
                //List<ProductPicture> prod = new List<ProductPicture>();
                var prod =products[i].ProductPicturees.Select(q=>q.Putch).ToList();
                modelJs.pictures=prod.ToArray();
                model_for_catalog.Add(modelJs);
            }
            ProductsGeneral product = new ProductsGeneral();
            product.modelJsAlpines=model_for_catalog;
            return View("katalog", product);
        }
    }   
}
