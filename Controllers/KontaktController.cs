using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class KontaktController : Controller
    {
        public IActionResult KontaktStart()
        {
            return View("Łączność");
        }
    }
}
