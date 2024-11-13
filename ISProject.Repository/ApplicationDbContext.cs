using ISProject.Domain;
using ISProject.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISProject.Repository
{
    public class ApplicationDbContext : IdentityDbContext<MusicStoreUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MusicRecord>()
                .HasMany(m => m.MusicRecordInOrders)
                .WithOne(m => m.MusicRecord)
                .HasForeignKey(m => m.MusicRecordId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<MusicRecord>()
                .HasMany(m => m.MusicRecordsInShoppingCart)
                .WithOne(m => m.MusicRecord)
                .HasForeignKey(c => c.MusicRecordId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ShoppingCart>()
                .HasMany(c => c.MusicRecordsInShoppingCart)
                .WithOne(m => m.ShoppingCart)
                .HasForeignKey(m => m.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Order>()
                .HasMany(o => o.MusicRecordsInOrder)
                .WithOne(m => m.Order)
                .HasForeignKey(m => m.OrderId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        public DbSet<MusicRecord> MusicRecords { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<MusicRecordInOrder> MusicRecordsInOrder { get; set; }
        public virtual DbSet<MusicRecordInShoppingCart> MusicRecordsInShoppingCart { get; set; }
    }
}
