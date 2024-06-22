using Microsoft.EntityFrameworkCore;
using Warehouse_API.Entities;

namespace Warehouse_API
{
    public class WarehouseDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<RFIDTag> RFIDTags { get; set; }
        public DbSet<Logs> Logs { get; set; }

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasOne(p => p.RFIDTag)
            .WithOne(rt => rt.Product)
            .HasForeignKey<RFIDTag>(rt => rt.Id);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.RFIDTagId)
                .IsUnique();

            modelBuilder.Entity<RFIDTag>()
                .HasOne(rt => rt.Product)
                .WithOne(p => p.RFIDTag)
                .HasForeignKey<RFIDTag>(rt => rt.ProductId);
        }
    }
}
