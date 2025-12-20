namespace Backend_asp.net.Models.DataBaseModel



    // Промежуточный класс для БД,
    // Класс для соединения Product and PropertyBicycle;
{
    public class ProductCategory
    {
        // Свойства класса;
        #region
        public int Id { get; set; }

        // Свойство для соединения класса PropertyBicycle и Product
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        #endregion

        // Навигационные свойства;
        public Product? Product { get; set; }
        public Category? Category { get; set; }
    }
}
