namespace Backend_asp.net.Models.Intermediate_class
{

    // Промежуточный класс для корзины и товара для Basket and Product;
    public class BasketProduct
    {
        // Свойства класса;
        #region
        public int Id { get; set; }
        private DateTime CreateTimeElementProduct { get; set; } = DateTime.Now;
        #endregion

        //Навигационные свойства для корзины и для продуктов;
        #region
        public int ProductID { get; set; }
        public int BasketID { get; set; }

        public Basket? Basket { get; set; }
        public Product? Product { get; set; }
        #endregion
    }
}
