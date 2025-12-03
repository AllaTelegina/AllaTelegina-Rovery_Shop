using Backend_asp.net.Models.DataBaseModel;

namespace Backend_asp.net.Models
{

    // класс для создания свойств товаров;
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelCategory {  get; set; }

        // Связь с продуктами через промежуточный класс ProductCategory;
        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
