namespace Backend_asp.net.Models.Intermediate_class
{
    public class PropertyTypeValue
    {
        public int Id { get; set; }
        public string Value { get; set; } = "No result";

        public int PropertyTypeId {  get; set; }
        public PropertyType PropertyType { get; set; }=new PropertyType {Name="Noname" };

        public int CategoryId { get; set; }
        public Category Category { get; set; }=new Category();
    }
}
