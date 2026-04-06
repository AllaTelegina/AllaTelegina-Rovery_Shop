using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{
    // Это класс механических велосипедов
    public class Product
    {
        // Свойства класса;
        #region
        public int Id { get; set; }

        // Свойства согласно магазина;
        public string Name { get; set; } = "Default";
        public string SkuNumber { get; set; } = "Default";                    // SKU номер магазина для этой серии
        public decimal Prise { get; set; }                                    // цена нашего магазина;
        public decimal Lastprise { get; set; }                                // последняя установленная цена;
        public DateTime CreateRoverMechTime { get; set; } = DateTime.Now;
        public StatusProductEnum Status {  get; set; }                        //Перечесление;
        public string BriefDiscription { get; set; } = "Default";             // Короткое описание;
        public string FullDiscription { get; set; } = "Default";              // Полное описание товара;
        public decimal Rating {  get; set; }                                  // Рейтинг объекта;

        // Свойство которое будет записываться по результату значения ProductCategory;
        public int IndicateCategory { get; set; } = 0;

        // Свойства непосредственно для описания на странице;
        public string? Color { get; set; } = "Default";

        //HACK: нужно разобраться с этим свойством, его я не использую, а использую для фото другой класс;
        //NOTE: это свойство, через которое я передаю массив адрес фотографий в cart_produkt.cshtml;
        #region
        public ICollection <string> Images { get; set; } = new List<string>();
        #endregion

        // Связь с корзиной товара через промежуточный class BacketProduct;
        #region
        public ICollection<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
        #endregion

        // Связь с категориями товаров через промежуточный класс ProductCategory;
        #region
        public ICollection<ProductCategory> ProductCategoryes { get; set; }=new List<ProductCategory>();
        #endregion

        // Связь продукта с ProductPicture;
        #region
        public ICollection<ProductPicture> ProductPicturees { get; set; } = new List<ProductPicture>();
        #endregion

        // Связь с категориями товаров через промежуточный класс ProductKeyFeature;
        #region
        public ICollection<ProductKeyFeature> ProductKeyFeatures { get; set; } = new List<ProductKeyFeature>();
        #endregion
    }
}
