namespace InventoryManagement.Shared;

public record ProductDetail
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Code))
            throw new InvalidOperationException("Code is required");
        if (Code.Length > 50)
            throw new InvalidOperationException("Code must be less than or equal to 50 characters");
        if (string.IsNullOrWhiteSpace(Name))
            throw new InvalidOperationException("Name is required");
        if (Name.Length > 100)
            throw new InvalidOperationException("Name must be less than or equal to 100 characters");
        if (Quantity < 0)
            throw new InvalidOperationException("Quantity must be greater than or equal to 0");
        if (Price < 0.01m)
            throw new InvalidOperationException("Price must be greater than or equal to 0.01");
        if (Price > 10000)
            throw new InvalidOperationException("Price must be less than or equal to 10000");
    }
}
public interface IProductService
{
    Task<ServiceResult<IEnumerable<ProductDetail>>> GetProductsAsync();
    Task<ServiceResult<ProductDetail>> GetProductAsync(int id);
    Task<ServiceResult<IEnumerable<ProductDetail>>> SearchProductsByCodeAsync(string code);
    Task<ServiceResult<IEnumerable<ProductDetail>>> SearchProductsByNameAsync(string name);
    Task<ServiceResult<ProductDetail>> AddProductAsync(ProductDetail product);
    Task<ServiceResult<ProductDetail>> UpdateProductAsync(ProductDetail product);
    Task<ServiceResult<bool>> DeleteProductAsync(int id);

}