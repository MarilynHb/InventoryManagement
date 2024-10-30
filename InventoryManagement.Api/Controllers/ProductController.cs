using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Shared;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    #region Constructor
    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }
    readonly IProductService productService;
    #endregion

    [HttpGet]
    public ActionResult<List<ProductDetail>> GetAllProducts()
    {
        return Ok(productService.GetProducts());
    }
}