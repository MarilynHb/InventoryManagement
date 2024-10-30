using InventoryManagement.Data.Context;
using InventoryManagement.Shared;

namespace InventoryManagement.Server;
public class ProductService : IProductService
{
    #region Constructor
    public ProductService(IInventoryEntities context)
    {
        this.context = context;
    }
    readonly IInventoryEntities context;
    #endregion

    public IEnumerable<ProductDetail> GetProducts()
    {
        return (from p in context.ProductEntities
                select new ProductDetail
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).AsEnumerable();
    }
}
