using CartingService.BLL.Model;

namespace CartingService.BLL.Services;

public interface ICartService
{
    CartModel GetCart(string cartId);
    IEnumerable<CartModel> GetCarts();
    CartModel AddCart(CartModel cart);
    void UpdateCart(string id, CartModel cart);
    void DeleteCart(string cartId);
}
