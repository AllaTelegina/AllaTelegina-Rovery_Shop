namespace Backend_asp.net.Models
{
    public class MainCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        //connection the table of a product;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
