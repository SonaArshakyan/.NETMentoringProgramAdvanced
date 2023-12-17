using CartingService.BLL.Model;
using CartingService.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.PresentationLayer;

[Authorize(Roles = "Manager,Buyer")]
[Route("api/v1/carts")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // GET: api/v1/Carts/1
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetCart([FromQuery] string? cartKey)
    {
        return Ok(_cartService.GetCarts(cartKey));
    }

    // POST: api/v1/Carts
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddCart([FromBody] CartModel cart)
    {
        try
        {
        return Ok(_cartService.AddCart(cart));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/v1/Carts/1
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateCart([FromRoute] string id, [FromBody] CartModel cart)
    {
        _cartService.UpdateCart(id, cart);
        return NoContent();
    }

    // DELETE: api/v1/Carts/1
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([FromRoute] string id, [FromQuery] string cartKey)
    {
        _cartService.DeleteCart(id, cartKey);
        return NoContent();
    }
}

[Route("api/v2/carts")]
[ApiController]
public class CartsControllerV2 : ControllerBase
{
    private readonly ICartService _cartService;
    public CartsControllerV2(ICartService cartService)
    {
        _cartService = cartService;
    }

    // GET: api/v2/Carts
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetCart()
    {
        return Ok(_cartService.GetCarts());
    }
}
