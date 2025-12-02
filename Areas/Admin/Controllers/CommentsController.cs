using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult CommentsMetod()
        {
            return View("~/Areas/Admin/Views/Comments/index.cshtml");
        }
    }
}
