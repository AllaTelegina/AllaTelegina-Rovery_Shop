using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{
    public class PropertyType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<PropertyTypeValue> PropertyTypeValues { get; set; } = new List<PropertyTypeValue>();
    }
}
