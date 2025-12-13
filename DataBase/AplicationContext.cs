using Backend_asp.net.Models;
using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.DataBase
{
    public class AplicationContext:DbContext
    {
        // создание в Db таблицу пользователей
        public DbSet<UserRovery> userRoverys { get; set; }
        // создание в Db таблицу товаров механические велосипеды
        public DbSet<Product> products { get; set; }
        // создание в Db таблицу карточку товаров
        public DbSet<Basket> baskets { get; set; }
        // Категория товаров;
        public DbSet<Category> categorys{ get; set; }

        // Добовление таблицы свойств для велосипедов и промежуточной таблицы;
        public DbSet<ProductCategory> resultPropertyBicycles { get; set; }
        public DbSet<BasketProduct> basketProducts { get; set; }

        public  AplicationContext(DbContextOptions<AplicationContext> option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRovery>()
                .HasMany(a => a.Baskets)
                .WithOne(c => c.UserRovery)
                .HasForeignKey(c => c.UserRoveryId);

            // Насройка связи корзина и продукт;
            modelBuilder.Entity<BasketProduct>()
                .HasKey(c => new{ c.BasketID, c.ProductID});
            modelBuilder.Entity<BasketProduct>()
                .HasOne(t=>t.Basket)
                .WithMany(c=>c.BasketProducts)
                .HasForeignKey(x=>x.BasketID);
            modelBuilder.Entity<BasketProduct>()
                .HasOne(a=>a.Product)
                .WithMany(b=>b.BasketProducts)
                .HasForeignKey(c=>c.ProductID);

            // настройка для product чтоб небыло путаницы в ключах;
            modelBuilder.Entity<Product>()
                .HasOne(t => t.MainCategory)
                .WithMany()
                .HasForeignKey(a=>a.IsCategory)
                .OnDelete(DeleteBehavior.Restrict);


            // Настройка связи продукт и категория;


        }


    }
}
