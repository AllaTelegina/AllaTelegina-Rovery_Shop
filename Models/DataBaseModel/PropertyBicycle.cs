namespace Backend_asp.net.Models.DataBaseModel
{

    // класс для создания свойств велосипеда;
    public class PropertyBicycle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Связь с велосипедами для которых эти свойства RoverMech;
        public ICollection <ResultPropertyBicycle> BicycleValues { get; set; }
    }
}
