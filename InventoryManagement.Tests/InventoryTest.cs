using InventoryManagement.Data.Context;
using InventoryManagement.Data.Models;
using InventoryManagement.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Tests.Base;

[TestClass]
public class InventoryTest
{
    protected InventoryDbContext DbContext { get; private set; } = null!;
    protected static DbContextOptions<InventoryDbContext> DbContextOptions { get; private set; } = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        DbContextOptions = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid}")
            .Options;

        DbContext = new InventoryDbContext(DbContextOptions);

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();

        SeedTestData();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        DbContext.Dispose();
    }

    private void SeedTestData()
    {
        var product = new Product
        {
            Id = TestData.SeedProduct.Id,
            Code = TestData.SeedProduct.Code,
            Name = TestData.SeedProduct.Name,
            Price = TestData.SeedProduct.Price,
            Quantity = TestData.SeedProduct.Quantity
        };

        DbContext.Add(product);
        DbContext.SaveChanges();
    }
}
