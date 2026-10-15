namespace Backend_asp.net.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public string Property { get; set;} = "Noname";
        public string Value { get; set;} = "Noname";

        // RU:Навигационное свойство для связи с Product;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        //  RU:Навигационное свойство для связи с Category;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
