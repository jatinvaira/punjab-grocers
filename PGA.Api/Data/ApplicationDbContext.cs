using Microsoft.EntityFrameworkCore;
using PGA.Api.Models;
using PGA.API.Models;

namespace PGA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Price column type to avoid truncation
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Precision 18, Scale 2
            modelBuilder.Entity<Checkout>(entity =>
            {
                // Specify the precision and scale for the TotalPrice column
                entity.Property(c => c.TotalPrice)
                      .HasColumnType("decimal(18,2)"); // Precision 18, Scale 2
            });
            // Seed data
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Apple", Description = "Description for Product1", ImageUrl = "apple.png", Price = 3.00M },
                new Product { Id = 2, Title = "Banana", Description = "Description for Product2", ImageUrl = "banana.png", Price = 3.50M },
                new Product { Id = 3, Title = "Bread", Description = "Description for Product3", ImageUrl = "bread.png", Price = 2.75M }
            );
        }
    }
}
