using System.Security.Claims;
using System.Security.Principal;
using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Controllers
{
    public class SendGoogleController : Controller
    {
        private readonly AplicationContext _context;
        public SendGoogleController (AplicationContext context)
        {
            _context=context;
        }
        // действие кнопки для отправки на Google для проверки imail, login пользователя;
        public async Task <IActionResult> SingGoogleServer()
        {
            // Формирую ссылку для того чтоб можно было отправить на сервер Google;
            var sendGoogle = Url.Action("ResponceGoogle","SendGoogle");
            var propertis = new AuthenticationProperties { RedirectUri = sendGoogle };
            return Challenge(propertis,GoogleDefaults.AuthenticationScheme);
        }

        // Метод, который будет получать после ответа от Google;
        public async Task <IActionResult> ResponceGoogle()
        {
            // проверяем авторизированного пользователя у себя на странице;
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Claims;

            // получаю значения из cookies
            var emailUser=claims.FirstOrDefault(x => x.Type==ClaimTypes.Email)?.Value;
            var nameUser=claims.FirstOrDefault(x=> x.Type==ClaimTypes.Name)?.Value;

            if (emailUser == null || nameUser == null)
                return View("registration");

            // Записываю пользователся в Cookies и записываю его в базу данных;
            var dbuserEmail=await _context.userRoverys.FirstOrDefaultAsync(x=>x.Email==emailUser);
            if (dbuserEmail==null)
            {
                var userAddDatabase = new UserRovery()
                {
                    Name = nameUser,
                    Email = emailUser,
                };
                await _context.userRoverys.AddAsync(userAddDatabase);
                _context.SaveChanges();
            };

            var identityObjectFor = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identityObjectFor.AddClaim(new Claim(ClaimTypes.Name, nameUser));
            identityObjectFor.AddClaim(new Claim(ClaimTypes.Email, emailUser ));

            // Создание Cookie которые будут от Google;
            CookieOptions opt = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            };
            Response.Cookies.Append("name", nameUser, opt);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identityObjectFor));
            return RedirectToAction("Index", "RegisterUser");
        }
    }
}
