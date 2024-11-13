namespace ISAdminApp.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Artist { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public bool InStock { get; set; }
        public string? ImageURL { get; set; }
    }
}
