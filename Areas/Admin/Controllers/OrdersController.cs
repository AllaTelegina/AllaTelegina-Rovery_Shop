using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult OrdersMetod()
        {
            return View("~/Areas/Admin/Views/Orders/index.cshtml");
        }
    }
}
