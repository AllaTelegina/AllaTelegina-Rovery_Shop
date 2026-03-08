namespace Backend_asp.net.Models.Intermediate_class
{
    public class ProductKeyFeature
        // промежуточный класс, межжу Product и KeyFeature;
    {
        public int Id { get; set; }
        public string Name { get; set; } = "No name";

        // Связь KeyFeature;
        public int IdProduct { get; set; }
        public Product? Product { get; set; }

        // Связь Product;
        public int IdKeyFeature { get; set; }
        public KeyFeature? KeyFeature { get; set; }
    }
}
