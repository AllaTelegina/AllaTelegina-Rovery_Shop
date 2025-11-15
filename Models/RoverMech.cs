using Backend_asp.net.Models.DataBaseModel;

namespace Backend_asp.net.Models
{
    // Это класс механических велосипедов
    public class RoverMech
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string NomerRoveryShop { get; set; }
        public string? RoverGender { get; set; }
        public string? ModelRoverMech { get; set; }
        public string? BrandRoverMech { get; set; }
        public DateTime CreateRoverMechTime { get; set; } = DateTime.Now;

        // Связь с карточкой товара;
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart {  get; set; }

        // Связь с свойствами велосипеда;
        public ICollection <ResultPropertyBicycle>PropertyValues { get; set; }
    }
}
