using Microsoft.AspNetCore.Mvc;

namespace Backend_asp.net.Controllers
{
    // TODO: Контроллер который вызывается, когда пользователь нажмет на кнопку из корзины купить;
    // Выдает страницу, в которой предоставляется пользователю выбрать, либо зарегистрироваться либо продолжить покупку;

    public class OrderController : Controller
    {
        public async Task <IActionResult> Checkout()
        {
            return View("login");
        }
    }
}
