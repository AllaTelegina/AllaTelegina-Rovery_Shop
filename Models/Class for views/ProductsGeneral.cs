using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models.Class_for_views
{
    public class ProductsGeneral
    {
        public ICollection <Category> ListCategorys { get; set; }=new List<Category>();
        public Product ProductFromAdmin { get; set; } = new Product ();
        public ICollection<ModelJsAlpine<Product>> modelJsAlpines { get; set; } =new List<ModelJsAlpine<Product>>();
    }
}
