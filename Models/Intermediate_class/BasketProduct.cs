namespace Backend_asp.net.Models.Intermediate_class
{

    // Промежуточный класс для корзины и товара для Basket and Product;
    public class BasketProduct
    {
        public int Id { get; set; }
        private DateTime CreateTimeElementProduct { get; set; } = DateTime.Now;

        //Навигационные свойства;
        public int BasketID { get; set; }
        public Basket Basket { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

    }
}
