using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{

    // RU:класс для создания свойств товаров;
    public class Category
    {
        // RU:Свойства класса;
        #region
        public int Id { get; set; }   
        public string Name { get; set; } = "Noname";
        #endregion

        // RU: Связь  один ко многим с таблицей PropertyValue;
        public ICollection<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();
    }
}
