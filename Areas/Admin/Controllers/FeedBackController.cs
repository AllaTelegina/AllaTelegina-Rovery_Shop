using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class FeedBackController : Controller
    {
        public IActionResult FeedBackMethod()
        {
            return View("~/Areas/Admin/Views/Feedback/index.cshtml");
        }
    }
}
