using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models.Static_classes
{
    //RU:  Идея создания этого static класса, для того, чтоб можно было добовлять методы в него, и вызывать их в других классах, без создания экземпляра класса;
    static class AddStaticMethod
    {
        //RU: Метод который перебирет List <Features> и возвратит List<PropertyValue> в метод ProductService для добовления в БД;
        static public ICollection<PropertyValue> CreatePropertyValueFromFeatures (List<Features> featrures, Category category, Product product)
        {
            ICollection<PropertyValue> propertyValues=new List<PropertyValue>(); 
            foreach (var feat in featrures)
            {
                propertyValues.Add(new PropertyValue
                {
                    Property = feat.key,
                    Value = feat.value,
                    Category = category,
                    Product = product
                });
            }
            return propertyValues;
        }
    }
}
