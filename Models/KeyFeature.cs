using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Models
{
    // Класс, заполнение основных свойств, либо особенных свойств товара, который будет отображаться как экслюзивные либо как изысканные свойства;
    public class KeyFeature
    {
        // Свойства класса;
        #region
        public int Id { get; set; }
        public string Name { get; set; } = "No name";    // Имя фичи;
        public DateTime CreateKeyFeature {  get; set; }=DateTime.Now;
        #endregion

        // соединение с промежуточным ProductKeyFeature;
        #region
        public ICollection<ProductKeyFeature> ProductKeyFeatures { get; set; }=new List<ProductKeyFeature>();
        #endregion
    }
}
