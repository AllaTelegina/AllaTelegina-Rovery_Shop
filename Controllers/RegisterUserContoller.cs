using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Controllers
{
    public class RegisterUserController : Controller
    {
        private readonly AplicationContext _context;
        // конструктор для создания AplicationContext;
        public RegisterUserController(AplicationContext context)
        {
            _context = context;
        }

        // Регистрация и создание нового пользователя в базе данных;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Register_User(DotnetModel model)
        {
            var hashpassword = new PasswordHasher<UserRovery>();
            var hashModel = new UserRovery();   // созданная модель для того чтоб можно было записывать hash;
            var user=model.UserRovery;

            // проверка модели есть ли в базе данных такой email;
            var SerchModelDB = await _context.userRoverys.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (SerchModelDB == null)
            {

                // проверка модели на валидность;
                if (!ModelState.IsValid)
                {
                    return View("Error");
                }
                hashModel.Email = user.Email;   // подал в модель для записи в BD email;
                hashModel.Password = hashpassword.HashPassword(hashModel, user.Password);
                hashModel.DateOfBirth = user.DateOfBirth;
                hashModel.Name = user.Name;
                hashModel.PhoneNumer = user.PhoneNumer;
                hashModel.LastName = user.LastName;
                hashModel.TimeCreateUserRovery = DateTime.Now;

                // Записываем в базу данных пользователя;
                _context.userRoverys.Add(hashModel);
                await _context.SaveChangesAsync();
                //Передача данных в cookes;
                Response.Cookies.Append("name", user.Name ?? "", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    // Установить время для жизненного цикла cookies;
                    //Expires = DateTime.Now.AddDays(7)
                });
                Response.Cookies.Append("gmailcookie", user.Email ?? "", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.Now.AddDays(7)
                });
            }
            else
            {
                ViewBag.Error = "This email address already exists";
                SerchModelDB = null;
                return View("registration");
            }
            return RedirectToAction("Index");
        }

        // Метод для того, чтоб еще раз перезагрузить контроллер
        public ActionResult Index()
        {
            return View("index");
        }

        // Метод для проверки пользователя при входе;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EntranceForUser(DotnetModel model, bool RememberMe = false)
        {
            var hashpassword = new PasswordHasher<UserRovery>();
            var imail = model.UserRovery.Email;
            var password = model.UserRovery.Password;
            if(imail==null || password == null)
                return View("ErrorViewModel");

            // Цыкл проверки, пока пользователь введет правильные данные;
            var userDB= _context.userRoverys.FirstOrDefault(x=> x.Email == imail);
            var gethashpass = hashpassword.VerifyHashedPassword(userDB, userDB.Password, password);
            if (gethashpass == PasswordVerificationResult.Failed)    // тут ошибка, которую нужно завтра отредактировать;
            {
                ViewBag.ValidPassword = "Nieprawidłowy login lub hasło!";
                return View("index");
            }
            // Если нажата кнопка запомнить
            if (RememberMe)
            {
                if (gethashpass == PasswordVerificationResult.Success)
                {
                    Response.Cookies.Append("name", userDB.Name ?? "", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.Now.AddDays(7)
                    });
                    Response.Cookies.Append("gmailcookie", userDB.Email ?? "", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.Now.AddDays(7)
                    });
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (gethashpass == PasswordVerificationResult.Success)
                {
                    Response.Cookies.Append("name", userDB.Name ?? "", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                    });
                    Response.Cookies.Append("gmailcookie", userDB.Email ?? "", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                    });
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // Открытие ссылки на востановление пороля;
        public ActionResult Open_ForgotPassword()
        {
            return View("Forgot_password");
        }

        // Отправка письма на почту с временным поролем;
        public async Task<ActionResult> SendMessagePost(String email)
        {
            return View();
        }

    }
}
