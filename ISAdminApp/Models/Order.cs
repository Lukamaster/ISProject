namespace ISAdminApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string? OwnerId { get; set; }
        public ICollection<Product>? MusicRecordsInOrder { get; set; }
    }
}
