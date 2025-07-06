using Microsoft.EntityFrameworkCore;
using RetailInventory_New.Models;

namespace RetailInventory_New.Data
{
    public class RetailDbContext : DbContext
    {
        public RetailDbContext(DbContextOptions<RetailDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure decimal precision for SQLite
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasConversion<double>()
                .HasPrecision(18, 2);

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Wireless Mouse",
                    Price = 29.99m,
                    StockQuantity = 100,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "T-Shirt",
                    Price = 19.99m,
                    StockQuantity = 50,
                    CategoryId = 2
                }
            );
        }
    }
}