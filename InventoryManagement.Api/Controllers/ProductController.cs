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
    public async Task<ActionResult<IEnumerable<ProductDetail>>> GetAllProducts()
    {
        var result = await productService.GetProductsAsync();
        return result.Success ? Ok(result.Data) : Problem(result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetail>> GetProduct(int id)
    {
        var result = await productService.GetProductAsync(id);
        return result.Success
            ? Ok(result.Data)
            : result.ErrorMessage!.Contains("not found")
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDetail>> AddProduct(ProductDetail product)
    {
        var result = await productService.AddProductAsync(product);
        return result.Success
            ? CreatedAtAction(nameof(GetProduct), new { id = result.Data!.Id }, result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDetail>> UpdateProduct(ProductDetail product)
    {
        var result = await productService.UpdateProductAsync(product);
        return result.Success
            ? Ok(result.Data)
            : result.ErrorMessage!.Contains("not found")
                ? NotFound(result.ErrorMessage)
                : BadRequest(result.ErrorMessage);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await productService.DeleteProductAsync(id);
        return result.Success
            ? NoContent()
            : result.ErrorMessage!.Contains("not found")
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
    }

    //[HttpGet("search")]
    //[HttpGet("searchbycode")]
}