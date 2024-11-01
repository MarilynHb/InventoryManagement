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
        {
            throw new InvalidOperationException("Code is required");
        }
    }
}
public interface IProductService
{
    Task<ServiceResult<IEnumerable<ProductDetail>>> GetProductsAsync();
    Task<ServiceResult<ProductDetail>> GetProductAsync(int id);
    Task<ServiceResult<ProductDetail>> AddProductAsync(ProductDetail product);
    Task<ServiceResult<ProductDetail>> UpdateProductAsync(ProductDetail product);
    Task<ServiceResult<bool>> DeleteProductAsync(int id);

}