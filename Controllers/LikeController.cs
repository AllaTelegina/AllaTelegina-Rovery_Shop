using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class LikeController : Controller
    {
        public IActionResult LikeStart()
        {
            return View("ulubione");
        }
    }
}
