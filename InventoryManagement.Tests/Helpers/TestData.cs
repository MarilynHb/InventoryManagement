using InventoryManagement.Shared;

namespace InventoryManagement.Tests.Helpers;
public static class TestData
{
    public static readonly ProductDetail SeedProduct = new()
    {
        Id = 1,
        Code = "TP-201",
        Name = "Test Product",
        Price = 10.5m,
        Quantity = 100
    };

    public static readonly ProductDetail ValidNewProduct = new()
    {
        Code = "TP-202",
        Name = "New Test Product",
        Price = 15.99m,
        Quantity = 50
    };
}