using CartingService.DAL.Entities;
using CartingService.DAL.Repositories;

namespace CartingService.BLL.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public Cart GetCart(string cartId)
    {
        return _cartRepository.GetCartById(cartId);
    }

    public IEnumerable<Cart> GetCarts()
    {
        return _cartRepository.GetCarts();
    }

    public Cart AddCart(Cart cart)
    {
       return  _cartRepository.AddCart(cart);
    }

    public void DeleteCart(string cartId)
    {
        _cartRepository.DeleteCart(cartId);
    }

    public void UpdateCart(string id, Cart cart)
    {
         _cartRepository.UpdateCart(id,cart);
    }
}
