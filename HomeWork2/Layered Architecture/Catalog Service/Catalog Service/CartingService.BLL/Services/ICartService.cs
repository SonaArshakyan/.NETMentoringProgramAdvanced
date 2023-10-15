using CartingService.DAL.Entities;

namespace CartingService.BLL.Services;

public interface ICartService
{
    Cart GetCart(string cartId);
    IEnumerable<Cart> GetCarts();
    Cart AddCart(Cart cart);
    void UpdateCart(string id, Cart cart);
    void DeleteCart(string cartId);
}
