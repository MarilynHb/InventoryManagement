using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Models;

namespace InventoryManagement.Data.Context;

public class InventoryDbContext : DbContext, IInventoryEntities
{ 
    public DbSet<Product> Products { get; set; } = null!;

    public IQueryable<Product> ProductEntities => Set<Product>();
    public Product? FindProduct(int id) => Find<Product>(id);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(local);Integrated Security=true;Initial Catalog=Inventory;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
        });
    }
}

public interface IInventoryEntities
{
    IQueryable<Product> ProductEntities { get; }
    Product? FindProduct(int id);
}