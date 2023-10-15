using CartingService.BLL.Model;
using CartingService.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.PresentationLayer;

[Route("api/v1/carts")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    public CartsController( ICartService cartService)
    {
            _cartService = cartService;
    }

    // GET: api/Carts
    [HttpGet]
    public IEnumerable<CartModel> Get()
    {
        return _cartService.GetCarts();
    }

    // GET: api/Carts/1
    [HttpGet("{id}")]
    public CartModel Get(string id)
    {
        return _cartService.GetCart(id);
    }

    // POST: api/Carts
    [HttpPost]
    [Produces("application/json")]
    public CartModel Post([FromBody] CartModel cart)
    {
        return _cartService.AddCart(cart);
    }

    // PUT: api/Carts/1
    [HttpPut("{id}")]
    public void Put(string id, [FromBody] CartModel cart)
    {
        _cartService.UpdateCart(id, cart);
    }

    // DELETE: api/Carts/1
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        _cartService.DeleteCart(id);
    }
}
