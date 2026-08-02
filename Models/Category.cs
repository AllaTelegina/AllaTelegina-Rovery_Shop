using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{

    // класс для создания свойств товаров;
    public class Category
    {
        // Свойства класса;
        #region
        public int Id { get; set; }   
        public string Name { get; set; } = "Noname";
        #endregion

        // Связь с продуктами через промежуточный класс ProductCategory;
        #region
        public ICollection<ProductCategory> ProductCategoryes { get; set; }=new List<ProductCategory>();
        #endregion

        // The setup connection with "Category" and "PropertyType" via "PropertyTypeValue"
        public ICollection<PropertyTypeValue>PropertyTypeValues { get; set; }=new List<PropertyTypeValue>();
    }
}
