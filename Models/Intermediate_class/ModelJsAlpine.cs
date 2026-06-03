namespace Backend_asp.net.Models.Intermediate_class
{
    public class ModelJsAlpine<T>
    // промежуточный класс, между классами который передает с представления js alpine в Product class;
    {
        public int id { get; set; }
        public string title { get; set; } = "no name";
        public string sku { get; set; } = "no name";
        public string category { get; set; } = "no name";
        public int categoryId { get; set; }              // передача id категории в представление;
        public decimal price { get; set; }
        public decimal oldPrice { get; set; }
        public double rating { get; set; }  
        public string status { get; set; } = "no name";  // передача значения enum в string;
        public int statusInt { get; set; }               // передача значения enum в int;
        public string[] keyFeatures { get; set; } = [];
        public string shortDesc { get; set; } = "no name";
        public string fullDesc { get; set; } = "no name";
        public List<Blog> descriptionBlocks { get; set; }= new List<Blog>(); // Получени List для сохранения блога;
        public Specs [] specs { get; set; } = [];  // свойство передачи технических характеристик товара;
        public string[] pictures { get; set; } = [];
        public string Create_time { get; set; } = "no name";        //Дата создания;
        public string Time_of_chenge { get; set; } = "no name";     // Дата изминения;
        public string[] images { get; set; } = [];  // принимает адреса картинок, которые приходят из views
                                                    // чтоб можно было определить установленную картинку;
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
