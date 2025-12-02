using Azure.Core;
using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Backend_asp.net.SendMessage;
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
                //if (!ModelState.IsValid)
                //{
                //    return View("Error");
                //}
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
        public async Task <IActionResult> EntranceForUser(DotnetModel model, bool RememberMe = false)
        {
            var hashpassword = new PasswordHasher<UserRovery>();
            var imail = model.UserRovery.Email;
            var password = model.UserRovery.Password;
            if(imail==null || password == null)
                return View("ErrorViewModel");

            // провверяю есть ли в базе данных;
            var userDB= await _context.userRoverys.FirstOrDefaultAsync(x=> x.Email == imail);
            if (userDB != null)
            {
                if (userDB.Password == "default" || userDB.Password == null)
                    return ErrorValidation();

                var gethashpass = hashpassword.VerifyHashedPassword(userDB, userDB.Password, password);
                if (gethashpass == 0)
                {
                    return ErrorValidation();
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
            }
            else
               return ErrorValidation();

            return RedirectToAction("Index");
        }

        // Метод который выводит в поля ошибки;
        private IActionResult ErrorValidation()
        {
            ViewBag.ValidPassword = "Nieprawidłowy login lub hasło!";
            return View("index");
        }

        // Открытие ссылки на востановление пороля;
        public ActionResult Open_ForgotPassword()
        {
            return View("Forgot_password");
        }

        // Отправка письма на почту с временным поролем;
        [HttpPost]
        public async Task<ActionResult> If_FogotPassword(UserRovery user)
        {
            var temporaryEmail=await _context.userRoverys.FirstOrDefaultAsync(x=>x.Email==user.Email);
            if (temporaryEmail == null)
            {
                ViewBag.Error = "Email is not exist";
                return View("forgot_password");
            }
            //Добовляем в модель Token и время действие этого токена;
            temporaryEmail.Token=Guid.NewGuid().ToString();
            temporaryEmail.TokenExpiry=DateTime.Now.AddMinutes(10);
            _context.SaveChanges();
            var link = Url.Action("BackToken", "RegisterUser", new {token = temporaryEmail.Token }, Request.Scheme);
            var body = $"Нажми сюда, для того чтоб сменить пороль: <a href='{link}'> смена пороля</a>";
            var subject = "Chenge password Rovery Shop";
            // Отправка письма на электронную почту
            await EmailSendService.SendAsynk(temporaryEmail.Email, subject, body);
            return View("index"); // можно сюда вставить страницу, где будет указано, что отправлено сообщение на почту и нужно подтвердить;
        }

        // Метод при котором будет проверяться Token, который возвратился обратно с письма;
        [HttpGet]
        public async Task<ActionResult> BackToken(string token)
        {
            var user=await _context.userRoverys.FirstOrDefaultAsync(x=>x.Token== token);
            if (user.TokenExpiry == null || user.TokenExpiry <= DateTime.Now)
            {
                user.TokenExpiry = null;
                return View("index");
            }

            // Создаю модель, которая будет передана в представление "new_password";
            var sendUser = new UserRovery()
            {
                Id=user.Id,
                Email=user.Email,
                Token=user.Token
            };
            return View("new_password", sendUser);
        }

        // Метод для замены пороля;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChengePassword(UserRovery user)
        {
            if (user == null)
            {
                return View("index");
            }
            // хэшер для пороля;
            var forgotpasswordhashee = new PasswordHasher<UserRovery>();
            var userDB = await _context.userRoverys.FirstOrDefaultAsync(x => x.Id == user.Id);
            var pas = forgotpasswordhashee.HashPassword(user, user.Password);
            userDB.Password = pas;
            _context.SaveChanges();

            //Cookies которые живут, только один сеанс;
            Response.Cookies.Append("name", userDB.Name, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            });
            Response.Cookies.Append("gmailcookie", userDB.Email, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            });
            return Redirect("Index");
        }

    }
}
