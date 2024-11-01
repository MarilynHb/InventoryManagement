using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Models;

namespace InventoryManagement.Data.Context;

public class InventoryDbContext : DbContext, IInventoryEntities
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    public IQueryable<Product> ProductEntities => Set<Product>();
    public Product? FindProduct(int id) => Find<Product>(id);
    public async Task<Product?> FindProductAsync(int id) => await FindAsync<Product>(id);

    public void Add(Product product) => Set<Product>().Add(product);
    public void Remove(Product product) => Set<Product>().Remove(product);
    public async Task SaveChangesAsync() => await base.SaveChangesAsync();

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
    void Add(Product product);
    void Remove(Product product);
    Task SaveChangesAsync();
}