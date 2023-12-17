using Microsoft.AspNetCore.Mvc;
using Catalog.Interfaces.Services;
using Catalog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
          _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// Get all Products // action => List of Products (List of Items filtration by category id and pagination)
    /// </summary>
    /// <response code="200">The Product list successfully got</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProducts([FromQuery] int categoryId)
    {
        var products = await _productService.GetAllProductsAsync(categoryId);
        
        return Ok( products);
    }

    /// <summary>
    /// Create a new Product // action => Add item
    /// </summary>
    /// <response code="201">The Product was created successfully. </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>

    [Authorize(Roles = "Manager")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var result = await _productService.CreateProductAsync(product);

        return CreatedAtAction(
            actionName: nameof(CreateProduct),
            value: result);
    }

    /// <summary>
    /// Delete Product // action => Delete item
    /// </summary>
    /// <response code="204">The Product is deleted successfully.</response>
    /// <response code="404">A Product is not found</response>
    /// <response code="500">A server fault occurred</response>

    [Authorize(Roles = "Manager")]
    [HttpDelete("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        await _productService.DeleteProductAsync(productId);
        return NoContent();
    }

    /// <summary>
    /// Update an existing Product // action => Update item
    /// </summary>
    /// <response code="204">The Product was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The Producti s not found</response>
    /// <response code="500">A server fault occurred</response>


    [Authorize(Roles = "Manager")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product  product)
    {
        await _productService.UpdateProductAsync(id, product);
        return NoContent();
    }
}
