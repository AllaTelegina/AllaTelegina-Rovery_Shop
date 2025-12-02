using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult UsersMethod()
        {
            return View("~/Areas/Admin/Views/Users/index.cshtml");
        }
    }
}
