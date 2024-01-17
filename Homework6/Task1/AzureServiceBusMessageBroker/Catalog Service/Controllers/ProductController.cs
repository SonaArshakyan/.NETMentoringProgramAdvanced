using Microsoft.AspNetCore.Mvc;
using Catalog.Interfaces.Services;
using Catalog.Models;
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace Catalog.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    private readonly TelemetryClient _telemetryClient;

    public ProductController(IProductService productService, ILogger<ProductController> logger, TelemetryClient telemetryClient)
    {
          _productService = productService;
        _logger = logger;
        _telemetryClient = telemetryClient;
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
        var correlationId = Activity.Current?.TraceId.ToString();

        _telemetryClient.TrackEvent("GetProducts", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });
        var products = await _productService.GetAllProductsAsync(categoryId);
        
        return Ok( products);
    }

    /// <summary>
    /// Create a new Product // action => Add item
    /// </summary>
    /// <response code="201">The Product was created successfully. </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var correlationId = Activity.Current?.TraceId.ToString();

        _telemetryClient.TrackEvent("CreateProduct", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });
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

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var correlationId = Activity.Current?.TraceId.ToString();

        _telemetryClient.TrackEvent("DeleteProduct", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });

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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product  product)
    {
        var correlationId = Activity.Current?.TraceId.ToString();

        _telemetryClient.TrackEvent("UpdateProduct", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });
        await _productService.UpdateProductAsync(id, product);
        return NoContent();
    }
}
