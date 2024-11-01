using InventoryManagement.Data.Models;
using InventoryManagement.Shared;

namespace InventoryManagement.Server;
public static class EntitiesMapper
{
    internal static ProductDetail MapToDetail(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        return new ProductDetail
        {
            Id = product.Id,
            Code = product.Code,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }
    internal static Product MapToEntity(ProductDetail detail)
    {
        if (detail == null) throw new ArgumentNullException(nameof(detail));

        return new Product
        {
            Id = detail.Id,
            Code = detail.Code,
            Name = detail.Name,
            Price = detail.Price,
            Quantity = detail.Quantity
        };
    }
}
