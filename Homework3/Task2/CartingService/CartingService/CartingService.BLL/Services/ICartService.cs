using CartingService.BLL.Model;

namespace CartingService.BLL.Services;

public interface ICartService
{
    CartModel GetCartByKey(string cartKey);
    IEnumerable<CartModel> GetCarts(string cartKey = "");
    CartModel AddCart(CartModel cart);
    void UpdateCart(string id, CartModel cart);
    void DeleteCart(string cartId, string cartKey);
}