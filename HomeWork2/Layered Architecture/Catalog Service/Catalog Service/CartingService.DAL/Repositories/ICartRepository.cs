using CartingService.DAL.Entities;

namespace CartingService.DAL.Repositories;

public interface ICartRepository
{
    Cart GetCartById(string cartId);
    IEnumerable<Cart> GetCarts();
    Cart AddCart(Cart cart);
    void UpdateCart(string id, Cart cart);
    void DeleteCart(string cartId);
}