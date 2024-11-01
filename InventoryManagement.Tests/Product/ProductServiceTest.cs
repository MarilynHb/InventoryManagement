using InventoryManagement.Data.Models;
using InventoryManagement.Server;
using InventoryManagement.Shared;
using InventoryManagement.Tests.Base;
using InventoryManagement.Tests.Helpers;

namespace InventoryManagement.Tests;

[TestClass]
public class ProductServiceTest : InventoryTest
{
    #region Constructor
    public ProductServiceTest()
    {
    }
    ProductService NewProductService() => new ProductService(DbContext);
    #endregion

    #region Get
    [TestMethod]
    public async Task GetProductAsync_ExistingProduct_ReturnsSuccess()
    {
        int productId = 1;

        var result = await NewProductService().GetProductAsync(productId);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(TestData.SeedProduct.Code, result.Data.Code);
    }

    [TestMethod]
    public async Task GetProductAsync_NonExistingProduct_ReturnsFailure()
    {
        int nonExistentId = 999;

        var result = await NewProductService().GetProductAsync(nonExistentId);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Data);
        Assert.IsTrue(result.ErrorMessage != null && result.ErrorMessage.Contains("not found"));
    }
    #endregion

    #region Add
    //CreateProduct_ShouldAddProduct
    [TestMethod]
    public async Task AddProductAsync_ValidProduct_ReturnsSuccess()
    {
        var newProduct = new ProductDetail
        {
            Code = "TEST-002",
            Name = "New Test Product",
            Price = 29.99m,
            Quantity = 50
        };

        var result = await NewProductService().AddProductAsync(newProduct);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreNotEqual(0, result.Data.Id);
        Assert.AreEqual(newProduct.Code, result.Data.Code);
    }

    [TestMethod]
    public async Task AddProductAsync_DuplicateCode_ReturnsFailure()
    {
        var duplicateProduct = new ProductDetail
        {
            Code = TestData.SeedProduct.Code,
            Name = "Duplicate Product",
            Price = 15.99m,
            Quantity = 25
        };

        var result = await NewProductService().AddProductAsync(duplicateProduct);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Data);
    }
    #endregion

    #region Update
    [TestMethod]
    public async Task UpdateProductAsync_ExistingProduct_ReturnsSuccess()
    {
        int productId = 1;
        var result = await NewProductService().GetProductAsync(productId);
        var original = result.Data;
        Assert.IsNotNull(original);

        var updatedProduct = new ProductDetail
        {
            Id = original.Id,
            Code = original.Code,
            Name = "Updated Product Name",
            Price = original.Price,
            Quantity = original.Quantity
        };
        result = await NewProductService().UpdateProductAsync(updatedProduct);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(updatedProduct.Name, result.Data.Name);
        Assert.AreNotEqual(original.Name, result.Data.Name);
    }

    [TestMethod]
    public async Task UpdateProductAsync_NonExistingProduct_ReturnsFailure()
    {
        var nonExistentProduct = new ProductDetail
        {
            Id = 999,
            Code = "TEST-999",
            Name = "Non-existent Product",
            Price = 9.99m,
            Quantity = 10
        };

        var result = await NewProductService().UpdateProductAsync(nonExistentProduct);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Data);
    }
    #endregion

    #region Delete
    [TestMethod]
    public async Task DeleteProductAsync_ExistingProduct_ReturnsSuccess()
    {
        int existingProductId = 1;

        var result = await NewProductService().DeleteProductAsync(existingProductId);

        Assert.IsTrue(result.Success);
        var deletedProduct = await DbContext.FindAsync<Product>(existingProductId);
        Assert.IsNull(deletedProduct);
    }

    [TestMethod]
    public async Task DeleteProductAsync_NonExistingProduct_ReturnsFailure()
    {
        int nonExistentId = 999;

        var result = await NewProductService().DeleteProductAsync(nonExistentId);

        Assert.IsFalse(result.Success);
    }
    #endregion
}
