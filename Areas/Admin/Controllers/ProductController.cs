using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductsView()
        {
            return View("~/Areas/Admin/Views/Products/index.cshtml");
        }
    }
}
