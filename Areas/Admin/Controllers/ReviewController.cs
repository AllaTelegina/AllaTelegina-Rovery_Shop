using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult ReviewMetod()
        {
            return View("~/Areas/Admin/Views/Reviews/index.cshtml");
        }
    }
}
