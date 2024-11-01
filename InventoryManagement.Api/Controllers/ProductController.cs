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

    [HttpPost("{id}")]
    public async Task<ActionResult<ProductDetail>> AddProduct(int id, ProductDetail product)
    {
        if (id != 0) throw new InvalidOperationException("New Product, can't have an id");
        var result = await productService.AddProductAsync(product);
        return result.Success
            ? CreatedAtAction(nameof(GetProduct), new { id = result.Data!.Id }, result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDetail>> UpdateProduct(int id, ProductDetail product)
    {
        //TODO Update with exisitng code - return valid error
        if (id != product.Id)
        {
            return BadRequest("URL id doesn't match product id");
        }
        var result = await productService.UpdateProductAsync(product);
        return result.Success
            ? Ok(result.Data)
            : result.ErrorMessage!.Contains("not found")
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
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