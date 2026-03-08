using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Backend_asp.net.Models;

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


