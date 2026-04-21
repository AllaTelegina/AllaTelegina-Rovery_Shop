using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend_asp.net.Models;
using System.Net;

namespace Backend_asp.net.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() // Тут можно поставить страницу для загрузки
    {
        // TODO: создаем ананимного пользователя, и передаем его на страницу;
        // 1. Создать модель ананимного пользователя;
        // 2. Передать пользователя через cookies в представление;
        if (!Request.Cookies.ContainsKey("IdName"))
        {
            var id_name=Guid.NewGuid().ToString();
            var user = new
            {
                IdName = id_name,
                Email="Default"
            };
            Response.Cookies.Append("IdName", user.IdName, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
            });
        }
        return View("~/dist/index.cshtml");     // прописать либо название страницц, либо относительный путь пример "~/dist/card_produkt.cshtml"
    }
    // и обязательно перезапустить проект с помощью комнды в terminals  "dotnet watch run" либо "dotnet run"

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult OpenPageCshtml()
    {
        return View("Error");
    }
}


