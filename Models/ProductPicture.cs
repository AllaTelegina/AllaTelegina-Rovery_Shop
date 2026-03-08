namespace Backend_asp.net.Models
{
    public class ProductPicture
    {
        // Поля класса;
        #region
        public int Id { get; set; }
        public string Putch { get; set; } = "notfund";
        public string NamePicture { get; set; } = "notfund";
        public DateTime TimeEditFile { get; set; }= DateTime.Now;
        public bool MainPhoto { get; set; } = false;
        #endregion

        // Связь с продуктом;
        #region
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        #endregion
    }
}
