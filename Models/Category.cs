using Backend_asp.net.Models.DataBaseModel;

namespace Backend_asp.net.Models
{

    // класс для создания свойств товаров;
    public class Category
    {
        // Свойства класса;
        #region
        public int Id { get; set; }
        public string Name { get; set; } = "Noname";
        public string? Description { get; set; } = "no name";        // Значение свойства;
        public string? Title { get; set; } = "no title";             // Огловление блоков;

        // Если задать "0" то это будет категория первая и так далее;
        public int LevelCategory {  get; set; }
        #endregion

        // Связь с продуктами через промежуточный класс ProductCategory;
        #region
        public ICollection<ProductCategory> ProductCategoryes { get; set; }=new List<ProductCategory>();
        #endregion
    }
}
