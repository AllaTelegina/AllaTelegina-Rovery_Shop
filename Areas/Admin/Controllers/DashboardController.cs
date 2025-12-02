using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    //[Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Dashboard/Index.cshtml");
        }
    }
}
