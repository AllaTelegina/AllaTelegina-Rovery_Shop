namespace Backend_asp.net.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public bool? IsDeleted { get; set; }=false;
        public DateTime Created { get; set; }=DateTime.Now;

        // связь с таблицей Механических велосипедов RoverMeches
        public ICollection<RoverMech>RoverMeches { get; set; }

        // связь с таблицей пользователя UserRovery
        public int UserRoveryId { get; set; }
        public UserRovery UserRovery { get; set; }
    }
}
