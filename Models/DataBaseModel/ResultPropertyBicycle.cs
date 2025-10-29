namespace Backend_asp.net.Models.DataBaseModel
{
    public class ResultPropertyBicycle
    {
        // Промежуточный класс для хранения информации в DB

        public int Id { get; set; }
        public string Value { get; set; }

        // Свойство для соединения класса PropertyBicycle и RoverMech
        public int RoverMechId { get; set; }
        public int PropertyBicycleId { get; set; }

        // Навигационные свойства;
        public RoverMech RoverMech { get; set; }
        public PropertyBicycle PropertyBicycle { get; set; }
    }
}
