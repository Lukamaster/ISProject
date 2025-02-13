namespace ISAdminApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string? OwnerId { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerAddress { get; set; }
        public ICollection<Product>? MusicRecordsInOrder { get; set; }
    }
}
