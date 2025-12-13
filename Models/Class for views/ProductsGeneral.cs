namespace Backend_asp.net.Models.Class_for_views
{
    public class ProductsGeneral
    {
        public ICollection <Category>? ListCategorys { get; set; }
        public Product? ProductFromAdmin { get; set; }
    }
}
