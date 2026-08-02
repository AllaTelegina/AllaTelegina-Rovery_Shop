namespace Backend_asp.net.Models.Intermediate_class
{
    public class ModelJsAlpine<T>
    // промежуточный класс, между классами который передает с представления js alpine в Product class;
    {
        public int? id { get; set; }
        public string category { get; set; } = "no name";
        public string title { get; set; } = "no name";
        public string sku { get; set; } = "no name";
        public decimal price { get; set; }
        public decimal oldPrice { get; set; }
        public double rating { get; set; }  
        public string status { get; set; } = "no name";  // передача значения enum в string;
        public List<string> colors { get; set; } = new List<string>();
        public string shortDesc { get; set; } = "no name";
        public string fullDesc { get; set; } = "no name";
        public int categoryId { get; set; }              // передача id категории в представление;
        public string statusInt { get; set; } = "no name";               // передача значения enum в int;
        public List<string> keyFeatures { get; set; } = new List<string>();
        public List<string> images { get; set; } = new List<string>();  // принимает адреса картинок, которые приходят из views
        public List<Specs> specs { get; set; } = new List<Specs>();  // свойство передачи технических характеристик товара;
        //Not used the propetrys below;
        public string createdAt { get; set; } = "no name";
        public string updatedAt { get; set; } = "no name";
        public List<string> descriptionBlocks { get; set; } = new List<string>();
        public List<string> descImages { get; set; } = new List<string>();
    }

    public class Specs
    {
        public string name { get; set; } = "no name";
        public List<Features> features { get; set; } = new List<Features>();
    }

    public class Features
    {
        public string key { get; set; } = "no name";
        public string value { get; set; } = "no name";
    }
}
