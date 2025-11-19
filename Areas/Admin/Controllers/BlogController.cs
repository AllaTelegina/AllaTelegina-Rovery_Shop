using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult BlogMetod()
        {
            return View("~/Areas/Admin/Views/Blog/index.cshtml");
        }
    }
}
