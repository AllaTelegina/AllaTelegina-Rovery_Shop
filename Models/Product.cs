using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{
    // Это класс механических велосипедов
    public class Product
    {
        public int Id { get; set; }

        // Свойства согласно магазина;
        public string Name { get; set; } = "Default";
        public string SkuNumber { get; set; } = "Default";                    // SKU номер магазина для этой серии
        public decimal Prise { get; set; }                          // цена нашего магазина;
        public decimal Lastprise { get; set; }                      // последняя установленная цена;
        private DateTime CreateRoverMechTime { get; set; } = DateTime.Now;
        public StatusProductEnum Status {  get; set; }               //Перечесление;
        public string BriefDiscription { get; set; } = "Default";                 // Короткое описание;
        public string FullDiscription { get; set; } = "Default";                 // Полное описание товара;
        public decimal Rating {  get; set; }                        // Рейтинг объекта;


        // Свойства непосредственно для описания на странице;
        public string? RoverGender { get; set; }
        public string? ModelRoverMech { get; set; }
        public string? BrandRoverMech { get; set; }
        public string Color { get; set; } = "Default";
        public ICollection <string> Images { get; set; } = new List<string>();

        // Связь с корзиной товара через промежуточный class BacketProduct;
        public ICollection <BasketProduct>? BasketProducts { get; set; }

        // Связь с категориями товаров через промежуточный класс ProductCategory;
        public ICollection<ProductCategory>? ProductCategory { get; set; }

        // связь один ко многим через главную категорию
        // Главной котегории присвоино именно Id номер;
        public int IsCategory { get; set; }                    // присвоение категории;
        public Category MainCategory { get; set; }              //Навигационное свойство
    }
}
