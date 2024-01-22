using Basket.Interfaces.Services;
using Basket.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics;

namespace Basket.Controllers;

[Route("api/v1/basketItems")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> _logger;
    private readonly IBasketService _basketService;
    private readonly TelemetryClient _telemetryClient;


    public BasketController(ILogger<BasketController> logger, IBasketService basketService, TelemetryClient telemetryClient)
    {
        _logger = logger;
        _basketService = basketService;
        _telemetryClient = telemetryClient;
    }

    /// <summary>
    /// Get all Basket Items
    /// </summary>
    /// <response code="200">The Basket Items list successfully got</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBasketItems()
    {
        var correlationId = Activity.Current?.TraceId.ToString();
        _telemetryClient.TrackEvent("GetBasketItems", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });
        return Ok(await _basketService.GetAllItemsAstnc());
    }

    /// <summary>
    /// Create a new Basket Item
    /// </summary>
    /// <response code="201">The Basket Item was created successfully. </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>
    /// 
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBasketItem([FromBody] BasketItem basketItem)
    {
        var correlationId = Activity.Current?.TraceId.ToString();
        _telemetryClient.TrackEvent("CreateBasketItem", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });

        var result = await _basketService.AddItemInBasketAsync(basketItem);

        return CreatedAtAction(
            actionName: nameof(CreateBasketItem),
            value: result);
    }

    /// <summary>
    /// Delete Basket Item
    /// </summary>
    /// <response code="204">The Basket Item was deleted successfully.</response>
    /// <response code="404">A Basket Item is not found</response>
    /// <response code="500">A server fault occurred</response>

    [HttpDelete("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBasketItem(int itemId)
    {
        var correlationId = Activity.Current?.TraceId.ToString();
        _telemetryClient.TrackEvent("DeleteBasketItem", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });

        await _basketService.DeleteBasketItemAsync(itemId);
        return NoContent();
    }

    /// <summary>
    /// Update an existing Basket Item
    /// </summary>
    /// <response code="204">The Basket Item was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The Basket Item was not found for specified item id</response>
    /// <response code="500">A server fault occurred</response>

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBasketItem(int id, [FromBody] BasketItem item)
    {
        var correlationId = Activity.Current?.TraceId.ToString();
        _telemetryClient.TrackEvent("UpdateBasketItem", new Dictionary<string, string>
        {
            { "CorrelationId", correlationId }
        });

        await _basketService.UpdateBasketItemAsync(id, item);
        return NoContent();
    }
}
