using CartingService.DAL.Entities;
using CartingService.DAL.Data;

namespace CartingService.DAL.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDBContext _db;

    public CartRepository(ApplicationDBContext dBContext)
    {
        _db = dBContext;
    }

    public  Cart GetCartById(string cartId)
    {
        return _db.Carts.FindOne(c => c.Id == cartId);
    }

    public IEnumerable<Cart> GetCarts()
    {
        return _db.Carts.FindAll();
    }

    public Cart AddCart(Cart cart)
    {
        var cartId = Guid.NewGuid().ToString();
        cart.Id = cartId;
        _db.Carts.Insert(cart);
        return GetCartById(cartId);
    }

    public void UpdateCart(string id, Cart cart)
    {
        var entity = GetCartById(id);
        entity.Price = cart.Price;
        entity.Name = cart.Name;
        entity.Quantity = cart.Quantity;
        _db.Carts.Update(entity);
    }

    public void DeleteCart(string cartId)
    {
        var cart = GetCartById(cartId);
        if (cart != null)
        {
            _db.Carts.Delete(cartId);
        }
    }
}
