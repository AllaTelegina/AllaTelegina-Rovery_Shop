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
        // Фотографии для продукта;
        public DbSet<ProductPicture> productsPicture { get; set; }
        // Добовление таблицы KeyFeatyre;
        public DbSet<KeyFeature> keyFeatures { get; set; }

        // Добовление таблицы свойств для велосипедов и промежуточной таблицы;
        #region
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<BasketProduct> basketProducts { get; set; }
        public DbSet<ProductKeyFeature> productKeyFeatures { get; set; }
        #endregion

        // Это конструктор;
        public AplicationContext(DbContextOptions<AplicationContext> option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            // настройка связи продукт и категория;
            modelBuilder.Entity<ProductCategory>()
                .HasKey(c => new { c.ProductId, c.CategoryId });
            modelBuilder.Entity<ProductCategory>()
                .HasOne(a => a.Product)
                .WithMany(b => b.ProductCategoryes)
                .HasForeignKey(t => t.ProductId);
            modelBuilder.Entity<ProductCategory>()
                .HasOne(a => a.Category)
                .WithMany(b => b.ProductCategoryes)
                .HasForeignKey(c => c.CategoryId);

            // настройка связи продукт и картинка;
            modelBuilder.Entity<ProductPicture>()
                .HasOne(a => a.Product)
                .WithMany(b => b.ProductPicturees)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // настройка связи продукт и KeyFeature
            modelBuilder.Entity<ProductKeyFeature>()
                .HasKey(t => new { t.IdProduct, t.IdKeyFeature });
            modelBuilder.Entity<ProductKeyFeature>()
                .HasOne(c => c.Product)
                .WithMany(a => a.ProductKeyFeatures)
                .HasForeignKey(b => b.IdProduct);
            modelBuilder.Entity<ProductKeyFeature>()
                .HasOne(a => a.KeyFeature)
                .WithMany(b => b.ProductKeyFeatures)
                .HasForeignKey(c => c.IdKeyFeature);

            // Чтоб EF увидел поля значением "null";
            modelBuilder.Entity<Category>()
                .Property(a => a.Description)
                .IsRequired(false);
            modelBuilder.Entity<Category>()
                .Property(a => a.Title)
                .IsRequired(false);
        }
    }
}
