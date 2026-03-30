using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class CustomerBasketController : Controller
    {
        public IActionResult CustomerBasketStart()
        {
            return View("koszyk");
        }
    }
}
