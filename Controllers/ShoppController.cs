using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class ShoppController : Controller
    {
        public IActionResult Shoppmethod()
        {
            return View("katalog");
        }
    }   
}
