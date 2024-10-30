namespace InventoryManagement.Shared;

public record ProductDetail
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}

public interface IProductService
{
    IEnumerable<ProductDetail> GetProducts();
    //ProductDetail GetProductAsync(int id);
    //ProductDetail AddProductAsync(ProductDetail product);
    //ProductDetail UpdateProductAsync(ProductDetail product);
    //Task DeleteProductAsync(int id);
}