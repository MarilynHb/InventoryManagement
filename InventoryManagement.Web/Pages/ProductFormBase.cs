using InventoryManagement.Shared;
using Microsoft.AspNetCore.Components;

namespace InventoryManagement.Web.Pages;

public class ProductFormBase : ComponentBase
{
    [Inject]
    protected IProductService ProductService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public int? Id { get; set; }

    protected ProductDetail product = new() { Code = "", Name = "", Price = 0, Quantity = 0 };
    protected string Title => Id.HasValue ? "Edit Product" : "Add Product";

}