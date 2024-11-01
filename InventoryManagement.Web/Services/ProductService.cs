using System.Net.Http.Json;
using InventoryManagement.Shared;

namespace InventoryManagement.Web.Services;

public class ProductService : IProductService
{
    #region Constructor
    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    readonly HttpClient httpClient;
    const string apiPath = "api/product";
    #endregion

    #region Get Product
    public async Task<ServiceResult<IEnumerable<ProductDetail>>> GetProductsAsync()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<ProductDetail>>("api/product");
            return new ServiceResult<IEnumerable<ProductDetail>>
            {
                Success = true,
                Data = response
            };
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<IEnumerable<ProductDetail>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    public async Task<ServiceResult<ProductDetail>> GetProductAsync(int id)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<ProductDetail>($"api/product/{id}");
            return new ServiceResult<ProductDetail>
            {
                Success = true,
                Data = response
            };
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<ProductDetail>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    public async Task<ServiceResult<IEnumerable<ProductDetail>>> SearchProductsByCodeAsync(string code)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<ProductDetail>>($"api/product/code/{code}");
            return new ServiceResult<IEnumerable<ProductDetail>>
            {
                Success = true,
                Data = response
            };
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<IEnumerable<ProductDetail>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    public async Task<ServiceResult<IEnumerable<ProductDetail>>> SearchProductsByNameAsync(string code)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<ProductDetail>>($"api/product/code/{code}");
            return new ServiceResult<IEnumerable<ProductDetail>>
            {
                Success = true,
                Data = response
            };
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<IEnumerable<ProductDetail>>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    #endregion

    #region Add Product
    public async Task<ServiceResult<ProductDetail>> AddProductAsync(ProductDetail product)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(apiPath, product);
            if (response.IsSuccessStatusCode)
            {
                var addedProduct = await response.Content.ReadFromJsonAsync<ProductDetail>();
                return new ServiceResult<ProductDetail>
                {
                    Success = true,
                    Data = addedProduct
                };
            }
            else
            {
                return new ServiceResult<ProductDetail>
                {
                    Success = false,
                    ErrorMessage = response.ReasonPhrase
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<ProductDetail>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    #endregion

    #region Update Product
    public async Task<ServiceResult<ProductDetail>> UpdateProductAsync(ProductDetail product)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync(apiPath, product);
            if (response.IsSuccessStatusCode)
            {
                var updatedProduct = await response.Content.ReadFromJsonAsync<ProductDetail>();
                return new ServiceResult<ProductDetail>
                {
                    Success = true,
                    Data = updatedProduct
                };
            }
            else
            {
                return new ServiceResult<ProductDetail>
                {
                    Success = false,
                    ErrorMessage = response.ReasonPhrase
                };
            }
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<ProductDetail>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    #endregion

    #region Delete Product
    public async Task<ServiceResult<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"{apiPath}/{id}");
            return new ServiceResult<bool>
            {
                Success = response.IsSuccessStatusCode
            };
        }
        catch (HttpRequestException ex)
        {
            return new ServiceResult<bool>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
    #endregion
}
