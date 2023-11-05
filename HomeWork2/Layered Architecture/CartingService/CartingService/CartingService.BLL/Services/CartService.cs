using AutoMapper;
using CartingService.BLL.Model;
using CartingService.DAL.Repositories;
using CartingService.BLL.Mapping;
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

    public CartModel GetCart(string cartId)
    {
        var cart = _cartRepository.GetCartById(cartId);
        return _mapper.Map<CartModel>(cart);

    }

    public IEnumerable<CartModel> GetCarts()
    {
        var cartModels = _cartRepository.GetCarts();
        return cartModels.Select(c => _mapper.Map<CartModel>(c));
    }

    public CartModel AddCart(CartModel cartModel)
    {
        var cart = _cartRepository.AddCart(_mapper.Map<Cart>(cartModel));
        return _mapper.Map<CartModel>(cart);
    }

    public void DeleteCart(string cartId)
    {
        _cartRepository.DeleteCart(cartId);
    }

    public void UpdateCart(string id, CartModel cartModel)
    {
        var cart = _mapper.Map<Cart>(cartModel);
        _cartRepository.UpdateCart(id, cart);
    }
}