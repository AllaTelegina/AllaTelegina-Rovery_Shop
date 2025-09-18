using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class SimplePageTransition : Controller
    {
        public IActionResult Registration()
        {
            return View("registration");
        }
    }
}
