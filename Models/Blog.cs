namespace Backend_asp.net.Models
{
    public class Blog
        // Блог на страницы;
    {
        public int Id { get; set; }
        public string title { get; set; } = "no name"; // Название блока;
        public string text { get; set; } = "no name"; // Описание блока;
    }
}
