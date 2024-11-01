using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryManagement.Shared;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    #region Constructor
    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        this.productService = productService;
        this.logger = logger;
    }
    readonly IProductService productService;
    readonly ILogger<ProductController> logger;
    #endregion

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDetail>>> GetAllProducts()
    {
        logger.LogInformation("Getting all products");
        var result = await productService.GetProductsAsync();
        if (!result.Success)
        {
            logger.LogError("Failed to get all products. Error: {ErrorMessage}", result.ErrorMessage);
        }
        return result.Success ? Ok(result.Data) : Problem(result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetail>> GetProduct(int id)
    {
        logger.LogInformation("Getting product with id: {Id}", id);
        var result = await productService.GetProductAsync(id);
        if (!result.Success)
        {
            logger.LogError("Failed to get product {Id}. Error: {ErrorMessage}", id, result.ErrorMessage);
        }
        return result.Success
            ? Ok(result.Data)
            : result.IsNotFound
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDetail>> AddProduct(ProductDetail product)
    {
        logger.LogInformation("Adding new product: {@Product}", product);
        var result = await productService.AddProductAsync(product);
        if (!result.Success)
        {
            logger.LogError("Failed to add product. Error: {ErrorMessage}", result.ErrorMessage);
        }
        else
        {
            logger.LogInformation("Successfully added product with id: {Id}", result.Data!.Id);
        }
        return result.Success
            ? CreatedAtAction(nameof(GetProduct), new { id = result.Data!.Id }, result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDetail>> UpdateProduct(ProductDetail product)
    {
        logger.LogInformation("Updating product {Id}: {@Product}", product.Id, product);
        var result = await productService.UpdateProductAsync(product);
        if (!result.Success)
        {
            logger.LogError("Failed to update product {Id}. Error: {ErrorMessage}", product.Id, result.ErrorMessage);
        }
        else
        {
            logger.LogInformation("Successfully updated product {Id}", product.Id);
        }
        return result.Success
            ? Ok(result.Data)
            : result.IsNotFound
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        logger.LogInformation("Deleting product {Id}", id);
        var result = await productService.DeleteProductAsync(id);
        if (!result.Success)
        {
            logger.LogError("Failed to delete product {Id}. Error: {ErrorMessage}", id, result.ErrorMessage);
        }
        else
        {
            logger.LogInformation("Successfully deleted product {Id}", id);
        }
        return result.Success
            ? NoContent()
            : result.IsNotFound
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<ProductDetail>> GetProductByCode(string code)
    {
        logger.LogInformation("Searching for product with code: {Code}", code);
        var result = await productService.SearchProductsByCodeAsync(code);
        if (!result.Success)
        {
            logger.LogError("Failed to find product with code {Code}. Error: {ErrorMessage}", code, result.ErrorMessage);
        }
        return result.Success
            ? Ok(result.Data)
            : result.IsNotFound
                ? NotFound(result.ErrorMessage)
                : Problem(result.ErrorMessage);
    }

    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<ProductDetail>>> SearchProductsByName(string name)
    {
        logger.LogInformation("Searching for products with name containing: {Name}", name);
        var result = await productService.SearchProductsByNameAsync(name);
        if (!result.Success)
        {
            logger.LogError("Failed to search products by name {Name}. Error: {ErrorMessage}", name, result.ErrorMessage);
        }
        return result.Success
            ? Ok(result.Data)
            : Problem(result.ErrorMessage);
    }
}