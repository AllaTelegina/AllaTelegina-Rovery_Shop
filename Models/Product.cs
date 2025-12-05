using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{
    // Это класс механических велосипедов
    public class Product
    {
        public int Id { get; set; }

        // Свойства согласно магазина;
        public string Name { get; set; }
        public ICollection<Category> IsCategory { get; set; }    // присвоение категории;
        public string SkuNumber { get; set; }                     // SKU номер магазина для этой серии
        public double Prise { get; set; }                         // цена нашего магазина;
        public double Lastprise { get; set; }                     // последняя установленная цена;
        private DateTime CreateRoverMechTime { get; set; } = DateTime.Now;
        public StatusProductEnum Status {  get; set; }             //Перечесление;
        public string BriefDiscription { get; set; }               // Короткое описание;
        public string FullDiscription {  get; set; }                // Полное описание товара;


        // Свойства непосредственно для описания на странице;
        public string? RoverGender { get; set; }
        public string? ModelRoverMech { get; set; }
        public string? BrandRoverMech { get; set; }
        public string Color {  get; set; }
        public ICollection <string> Images { get; set; }

        // Связь с корзиной товара через промежуточный class BacketProduct;
        public ICollection <BasketProduct> BasketProducts { get; set; }

        // Связь с категориями товаров через промежуточный класс ProductCategory;
        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
