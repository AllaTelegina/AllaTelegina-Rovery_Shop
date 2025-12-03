using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models

    // Класс для описания корзины товара
{
    public class Basket
    {
        public int Id { get; set; }
        // Проверить удаленный либо нет, либо действующая корзина;
        public bool? IsDeleted { get; set; }=false;
        public DateTime Created { get; set; }=DateTime.Now;

        // Связь с таблицей продукты, через промежуточный class BacketProduct;
        public ICollection<BasketProduct>BasketProducts { get; set; }

        // навигационные свойства для class UserRovery;
        public int UserRoveryId { get; set; }
        public UserRovery UserRovery { get; set; }
    }
}
