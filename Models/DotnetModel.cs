namespace Backend_asp.net.Models
{
    public class DotnetModel
    {
        public UserRovery? UserRovery { get; set; }
        // основная модель продукта;
        public List<Product> model_products { get; set; } = new List<Product>();
    }
}
