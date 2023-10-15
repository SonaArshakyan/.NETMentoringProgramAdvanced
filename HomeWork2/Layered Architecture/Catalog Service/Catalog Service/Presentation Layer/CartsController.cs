using CartingService.BLL.Services;
using CartingService.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.PresentationLayer;

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
    public IEnumerable<Cart> Get()
    {
        return _cartService.GetCarts();
    }

    // GET: api/Carts/1
    [HttpGet("{id}")]
    public Cart Get(string id)
    {
        return _cartService.GetCart(id);
    }

    // POST: api/Carts
    [HttpPost]
    [Produces("application/json")]
    public Cart Post([FromBody] Cart cart)
    {
        return _cartService.AddCart(cart);
    }

    // PUT: api/Carts/1
    [HttpPut("{id}")]
    public void Put(string id, [FromBody] Cart cart)
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
