using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult ReportsMethod()
        {
            return View("~/Areas/Admin/Views/Reports/index.cshtml");
        }
    }
}
