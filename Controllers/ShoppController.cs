using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend_asp.net.Controllers
{
    public class ShoppController : Controller
    {
        private readonly IProductService _serv;

        public ShoppController(IProductService serv)
        {
            _serv = serv;
        }

        public async Task  <IActionResult> Shoppmethod()
        {
            var product = await _serv.ModelSendInViews();
            return View("katalog", product);
        }

        //TODO: RU: Переходит из katalog.cshtml in card_product.cshtml;
        public async Task<IActionResult> KartProductStart(int id)
        {
            var productGeneral = await _serv.GetProductById(id);
            return View("card_produkt", productGeneral);
        }
    }
}
