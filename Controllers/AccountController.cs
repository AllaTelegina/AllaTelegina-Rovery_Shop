using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccountStart()
        {
            return View("conto");
        }
    }
}
