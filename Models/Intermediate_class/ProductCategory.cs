namespace Backend_asp.net.Models.DataBaseModel
{
    // Промежуточный класс, который будет соединять Product и Category;
    public class ProductCategory
    {
        // Свойства класса;
        #region
        // Свойство для соединения класса PropertyBicycle и Product
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        #endregion

        // Навигационные свойства;
        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
