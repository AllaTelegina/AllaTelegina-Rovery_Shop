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
        public DateTime? Time_of_chenge { get; set; }                         // Дата и время изменения;
        public StatusProductEnum Status {  get; set; }                        //Перечесление;
        public string BriefDiscription { get; set; } = "Default";             // Короткое описание;
        public string FullDiscription { get; set; } = "Default";              // Полное описание товара;
        public decimal Rating {  get; set; }                                  // Рейтинг объекта;
        public int Quantity { get; set; } = 1;                                // Количество товара;

        // The navigation property for EF. Where discription the table of are main category;
        public MainCategory? MainCategory { get; set; }   // It's properti, where write main category, example "Bike" or "ElectroBike" and other; 
        public int MainCategoryId { get; set; }     // Navi property;

        // Свойства непосредственно для описания на странице;
        public string? Color { get; set; } = "Default";

        //HACK: нужно разобраться с этим свойством, его я не использую, а использую для фото другой класс;
        //NOTE: это свойство, через которое я передаю массив адрес фотографий в cart_produkt.cshtml;
        public ICollection <string> Images { get; set; } = new List<string>();
        #endregion

        // The connect Product with ProductCategory;
        #region
        public ICollection<ProductCategory> ProductCategoryes { get; set; }=new List<ProductCategory>();
        #endregion

        // Связь с категориями товаров через промежуточный класс ProductKeyFeature;
        #region
        public ICollection<ProductKeyFeature> ProductKeyFeatures { get; set; } = new List<ProductKeyFeature>();
        #endregion

        // The connect Product and PropertyTypeValue;


        // I should, to sort through under;

        // Связь с корзиной товара через промежуточный class BacketProduct;
        #region
        public ICollection<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();
        #endregion

        // Связь продукта с ProductPicture;
        #region
        public ICollection<Photoproduct> ProductPicturees { get; set; } = new List<Photoproduct>();
        #endregion
    }
}
