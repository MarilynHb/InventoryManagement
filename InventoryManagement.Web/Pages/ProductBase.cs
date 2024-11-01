using InventoryManagement.Shared;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages;

public class ProductBase : ComponentBase
{
    [Inject]
    protected IProductService ProductService { get; set; } = default!;

    protected IEnumerable<ProductDetail> Products { get; set; } = [];
    protected string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ProductService.GetProductsAsync();
        if (result.Success && result.Data != null)
        {
            Products = result.Data;
            ErrorMessage = null;
        }
        else
        {
            ErrorMessage = result.ErrorMessage; 
        }
    }
}