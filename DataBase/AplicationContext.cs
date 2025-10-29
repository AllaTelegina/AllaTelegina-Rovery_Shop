using Backend_asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.DataBase
{
    public class AplicationContext:DbContext
    {
        // создание в Db таблицу пользователей
        public DbSet<UserRovery> userRoverys { get; set; }
        // создание в Db таблицу товаров механические велосипеды
        public DbSet<RoverMech> roverMechs { get; set; }
        // создание в Db таблицу карточку товаров
        public DbSet<ShoppingCart> shoppingCarts { get; set; } 
        public  AplicationContext(DbContextOptions<AplicationContext> option) : base(option)
        {
        }
    }
}
