using AutoMapper;
using CartingService.BLL.Model;
using CartingService.DAL.Repositories;
using CartingService.DAL.Entities;

namespace CartingService.BLL.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public CartModel GetCartByKey(string cartKey)
    {
        var cart = _cartRepository.GetCartByKey(cartKey);
        return _mapper.Map<CartModel>(cart);
    }

    public IEnumerable<CartModel> GetCarts(string cartKey = "")
    {
        var cartModels = _cartRepository.GetCarts();
        if(string.IsNullOrEmpty(cartKey))
            cartModels = cartModels.Where(c => c.CartKey == cartKey);
        return cartModels.Select(c => _mapper.Map<CartModel>(c));
    }

    public CartModel AddCart(CartModel cartModel)
    {
        if(_cartRepository.GetCartByKey(cartModel.CartKey) == null)
        {
        var  cart = _cartRepository.AddCart(_mapper.Map<Cart>(cartModel));
        return _mapper.Map<CartModel>(cart);
        }
        else
            throw new Exception("Cart with this Key already exists");
    }

    public void DeleteCart(string cartId, string cartKey)
    {
        _cartRepository.DeleteCart(cartId, cartKey);
    }

    public void UpdateCart(string id, CartModel cartModel)
    {
        var cart = _mapper.Map<Cart>(cartModel);
        _cartRepository.UpdateCart(id, cart);
    }
}