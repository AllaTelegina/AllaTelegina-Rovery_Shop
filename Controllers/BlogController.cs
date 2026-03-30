using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult BlogStart()
        {
            return View("blog");
        }
    }
}
