using InventoryManagement.Data.Context;
using InventoryManagement.Data.Models;
using InventoryManagement.Shared;
using Microsoft.EntityFrameworkCore;

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
     
    #region Get
    public async Task<ServiceResult<IEnumerable<ProductDetail>>> GetProductsAsync()
    {
        try
        {
            var query = await (from x in context.ProductEntities select x).AsNoTracking().ToListAsync();
            var products = query.Select(EntitiesMapper.MapToDetail);
            return ServiceResult<IEnumerable<ProductDetail>>.Sucess(products);
        }
        catch (Exception ex)
        {
            return ServiceResult<IEnumerable<ProductDetail>>.Failure($"Failed to retrieve products: {ex.Message}");
        }
    }
    public async Task<ServiceResult<ProductDetail>> GetProductAsync(int id)
    {
        try
        {
            var product = await context.ProductEntities.FirstOrDefaultAsync(p => p.Id == id);

            return product == null
                ? ServiceResult<ProductDetail>.Failure($"Product with ID {id} not found")
                : ServiceResult<ProductDetail>.Sucess(EntitiesMapper.MapToDetail(product));
        }
        catch (Exception ex)
        {
            return ServiceResult<ProductDetail>.Failure($"Failed to retrieve product: {ex.Message}");
        }
    }
    #endregion

    #region Add
    public async Task<ServiceResult<ProductDetail>> AddProductAsync(ProductDetail productDetail)
    {
        try
        {
            if (productDetail == null)
                return ServiceResult<ProductDetail>.Failure("Product details cannot be null");

            productDetail.Validate();

            var existingCode = (from x in context.ProductEntities where x.Code == productDetail.Code select x).FirstOrDefault();
            if (existingCode != null)
                return ServiceResult<ProductDetail>.Failure($"Product with code {productDetail.Code} already exists");

            var entity = EntitiesMapper.MapToEntity(productDetail);
            context.Add(entity);
            await context.SaveChangesAsync();

            return ServiceResult<ProductDetail>.Sucess(EntitiesMapper.MapToDetail(entity));
        }
        catch (Exception ex)
        {
            return ServiceResult<ProductDetail>.Failure($"Failed to add product: {ex.Message}");
        }
    }
    #endregion

    #region Update
    public async Task<ServiceResult<ProductDetail>> UpdateProductAsync(ProductDetail productDetail)
    {
        try
        {
            if (productDetail == null) return ServiceResult<ProductDetail>.Failure("Product details cannot be null");
            productDetail.Validate();

            var existingProduct = await context.ProductEntities.FirstOrDefaultAsync(p => p.Id == productDetail.Id);
            if (existingProduct == null)
                return ServiceResult<ProductDetail>.Failure($"Product with ID {productDetail.Id} not found");

            var existingCode = (from x in context.ProductEntities where x.Code == productDetail.Code && x.Id != productDetail.Id select x).FirstOrDefault();
            if (existingCode != null)
                return ServiceResult<ProductDetail>.Failure($"Product with code {productDetail.Code} already exists");

            existingProduct.Code = productDetail.Code;
            existingProduct.Name = productDetail.Name;
            existingProduct.Price = productDetail.Price;
            existingProduct.Quantity = productDetail.Quantity;

            await context.SaveChangesAsync();

            return ServiceResult<ProductDetail>.Sucess(EntitiesMapper.MapToDetail(existingProduct));
        }
        catch (Exception ex)
        {
            return ServiceResult<ProductDetail>.Failure($"Failed to update product: {ex.Message}");
        }
    }
    #endregion

    #region Delete
    public async Task<ServiceResult<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var product = context.FindProduct(id);

            if (product == null)
                return ServiceResult<bool>.Failure($"Product with ID {id} not found");
            context.Remove(product);
            await context.SaveChangesAsync();
            return ServiceResult<bool>.Sucess(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Failure($"Failed to delete product: {ex.Message}");
        }
    }
    #endregion
}