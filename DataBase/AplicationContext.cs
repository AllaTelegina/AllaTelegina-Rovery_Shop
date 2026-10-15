using Backend_asp.net.Models;
using Backend_asp.net.Models.Intermediate_class;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.DataBase
{
    public class AplicationContext:DbContext
    {
        // создание в Db таблицу пользователей
        public DbSet<UserRovery> UserRoverys { get; set; }
        // создание в Db таблицу товаров механические велосипеды
        public DbSet<Product> Products { get; set; }
        // создание в Db таблицу карточку товаров
        public DbSet<Basket> Baskets { get; set; }
        // Категория товаров;
        public DbSet<Category> Categorys{ get; set; }
        // Фотографии для продукта;
        public DbSet<Photoproduct> Photoproducts { get; set; }
        // Добовление таблицы KeyFeatyre;
        public DbSet<KeyFeature> KeyFeatures { get; set; }
        // Add Main Category;
        public DbSet<MainCategory> MainCategoryes { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        // Добовление таблицы свойств для велосипедов и промежуточной таблицы;
        #region
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<ProductKeyFeature> ProductKeyFeatures { get; set; }
        #endregion

        // Это конструктор;
        public AplicationContext(DbContextOptions<AplicationContext> option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyValue>()
                .HasOne(pv => pv.Product)
                .WithMany(p => p.PropertyValues)
                .HasForeignKey(pv => pv.ProductId);

            // RU:создание связи один ко многим между таблицами "Product" и "PropertyValue";
            modelBuilder.Entity<PropertyValue>()
                .HasOne(pv => pv.Category)
                .WithMany(c => c.PropertyValues)
                .HasForeignKey(pv => pv.CategoryId);

            // The setup connection with "Product" and "MainProduct";
            modelBuilder.Entity<Product>()
                .HasOne(a => a.MainCategory)
                .WithMany(b => b.Products)
                .HasForeignKey(a => a.MainCategoryId);

            modelBuilder.Entity<Basket>()
                .HasOne(a => a.UserRovery)
                .WithMany(c => c.Baskets)
                .HasForeignKey(c => c.UserRoveryId)
                .OnDelete(DeleteBehavior.Restrict);         // не удаляет корзины при удалении

            // Насройка связи корзина и продукт;
            modelBuilder.Entity<BasketProduct>()
                .HasKey(c => new{ c.BasketID, c.ProductID});
            modelBuilder.Entity<BasketProduct>()
                .HasOne(t => t.Basket)
                .WithMany(c => c.BasketProducts)
                .HasForeignKey(x => x.BasketID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BasketProduct>()
                .HasOne(a=>a.Product)
                .WithMany(b=>b.BasketProducts)
                .HasForeignKey(c=>c.ProductID);

            // настройка связи продукт и картинка;
            modelBuilder.Entity<Photoproduct>()
                .HasOne(a => a.Product)
                .WithMany(b => b.ProductPicturees)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // настройка связи продукт и KeyFeature
            modelBuilder.Entity<ProductKeyFeature>()
                .HasKey(t => new { t.IdProduct, t.IdKeyFeature });
            modelBuilder.Entity<ProductKeyFeature>()
                .HasOne(c => c.Product)
                .WithMany(a => a.ProductKeyFeatures)
                .HasForeignKey(b => b.IdProduct)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductKeyFeature>()
                .HasOne(a => a.KeyFeature)
                .WithMany(b => b.ProductKeyFeatures)
                .HasForeignKey(c => c.IdKeyFeature);
        }
    }
}
