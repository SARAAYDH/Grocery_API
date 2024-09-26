using Grocery.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Grocery.Data;

public class GroceryDbContext : DbContext
{
    public GroceryDbContext(DbContextOptions<GroceryDbContext> options)
    : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CategoryId);
    }
}
