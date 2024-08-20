using ISProject.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISProject.Repository
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MusicRecord> musicRecords { get; set; }
        public virtual DbSet<MusicRecordInOrder> musicRecordsInOrder { get; set; }
        public virtual DbSet<MusicRecordInShoppingCart> musicRecordsInShoppingCart { get; set; }
        public virtual DbSet<Order> orders { get; set; }
        public virtual DbSet<ShoppingCart> shoppingCarts { get; set; }

    }
}
