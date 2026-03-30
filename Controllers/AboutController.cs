using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutStart()
        {
            return View("about");
        }
    }
}
