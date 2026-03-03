using Microsoft.EntityFrameworkCore;
using DualON_Technologies_InventorySystem.Models;

namespace DualON_Technologies_InventorySystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Model)
                .IsUnique();
        }
    }
}