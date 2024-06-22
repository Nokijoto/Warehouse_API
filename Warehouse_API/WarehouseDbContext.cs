using Microsoft.EntityFrameworkCore;
using Warehouse_API.Entities;

namespace Warehouse_API
{
    public class WarehouseDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
        }
    }
}
