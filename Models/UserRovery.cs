namespace Backend_asp.net.Models
{
    public class UserRovery
    {
        // Свойства класса;
        #region
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName {  get; set; }
        public string Email { get; set; }
        public string? PhoneNumer { get; set; }  // номер телефона;
        public DateTime? DateOfBirth {  get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin { get; private set; } = false;         // Проверяет, является ли пользователь admin
        public DateTime? TimeIntrance { get; set; }     // Это время когда пользователь входит в систему;
        public bool? IsSignature { get; set; }=false;           // Имеется ли подписка у пользователя
        public bool? IsStatus { get; set; } =false;                 // Статус пользователя на сейчас. Он активен либо заблокированный;
        public string? IdName { get; set; }                         // Свойство для создания псевдо-пользователя;


        // Дополнительные парамеетры для заказа;
        public string? Country { get; set; }
        public string? PostCode { get; set; }  // почтовый индэкс;
        public string? Sity {  get; set; }
        public string? Street { get; set; }
        public string? Bilding { get; set; }
        public DateTime? TimeCreateUserRovery {  get; set; }

        // Параметры для подтверждения email пользователя;
        public string? Token { get; set; }
        public bool? IsConfirm { get; set; }=false;
        public DateTime? TokenExpiry { get; set; }
        #endregion

        // Конструкторы класса;
        #region
        public UserRovery()
        {
            Password = "default";
        }
        public UserRovery(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
        #endregion

        // Метод для определения админ прав
        #region
        public void IsAdminTrue()
        {
            IsAdmin = true;
        }
        #endregion

        // связь один ко многим с карточкой товаров
        public ICollection<Basket> Baskets { get; set; }=new List<Basket>();

    }
}
