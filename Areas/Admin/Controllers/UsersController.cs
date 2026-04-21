using Backend_asp.net.DataBase;
using Backend_asp.net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        // Открытие базы данных, определение context;
        private readonly AplicationContext _context;
        // Конструктор для создания контекста;
        public UsersController(AplicationContext context)
        {
            _context=context;
        }

        //____________________________________________________________________________________________________________//
        // Начало методов;
        public async Task <IActionResult> UsersMethod()
        {
            // Отражение всех пользователей в таблице;
            ICollection<UserRovery> viewsUser = new List<UserRovery>();
            viewsUser =await _context.userRoverys.ToListAsync();
            return View("~/Areas/Admin/Views/Users/index.cshtml", viewsUser);
        }
    }
}
