namespace Backend_asp.net.Models.Enams
{
    public static class StatusProduct
    {
        // Это словарь для enum StatusProductEnum.cs
        public static readonly Dictionary<StatusProductEnum, string> Name = new Dictionary<StatusProductEnum, string>
        {
            {StatusProductEnum.InStock, "В наличие"},
            {StatusProductEnum.OutOfStock, "Нет в наличие"},
            {StatusProductEnum.Pending, "Ожидается" }
        };

    }
}
