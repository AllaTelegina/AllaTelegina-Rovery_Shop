namespace Backend_asp.net.Models.DataBaseModel



    // Промежуточный класс для БД,
    // Класс для соединения Product and PropertyBicycle;
{
    public class ProductCategory
    {

        public int Id { get; set; }
        public string Value { get; set; }

        // Свойство для соединения класса PropertyBicycle и Product
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        // Навигационные свойства;
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
