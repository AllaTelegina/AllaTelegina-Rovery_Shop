using Backend_asp.net.Models.DataBaseModel;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models

    // Класс для описания корзины товара
{
    public class Basket
    {
        // Свойства класса;
        #region
        public int Id { get; set; }
        // Проверить удаленный либо нет, либо действующая корзина;
        public bool? IsDeleted { get; set; }=false;
        public DateTime Created { get; set; }=DateTime.Now;
        #endregion

        // навигационные свойства для class UserRovery;
        #region
        public int UserRoveryId { get; set; }       // Внешний ключ;
        public UserRovery? UserRovery { get; set; }
        #endregion

        // Связь с таблицей продукты, через промежуточный class BacketProduct;
        #region
        public ICollection<BasketProduct>BasketProducts { get; set; }= new List<BasketProduct>();
        #endregion
    }
}
