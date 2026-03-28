namespace Backend_asp.net.Models.Intermediate_class
{
    public class ModelJsAlpine<T>
    // промежуточный класс, между классами который передает с представления js alpine в Product class;
    {
        public string title { get; set; } = "no name";
        public string sku { get; set; } = "no name";
        public string category { get; set; } = "no name";
        public decimal price { get; set; }
        public decimal oldPrice { get; set; }
        public double rating { get; set; }  
        public string status { get; set; } = "no name";
        public string[] keyFeatures { get; set; } = [];
        public string shortDesc { get; set; } = "no name";
        public string fullDesc { get; set; } = "no name";
        public List<Blog> descriptionBlocks { get; set; }= new List<Blog>(); // Получени List для сохранения блога;
        public Specs [] specs { get; set; } = [];
    }

    public class Specs
    {
        public string name { get; set; } = "no name";
        public Features [] features { get; set; } = [];
    }

    public class Features
    {
        public string key { get; set; } = "no name";
        public string value { get; set; } = "no name";
    }
}
