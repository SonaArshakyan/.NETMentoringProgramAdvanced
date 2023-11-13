using Basket.Models;

namespace Basket.Interfaces.Services;

public interface IBasketService
{
    Task<BasketItem> AddItemInBasketAsync(BasketItem item);
    Task<List<BasketItem>> GetAllItemsAstnc();
    Task UpdateBasketItemAsync(int id, BasketItem item);
    Task DeleteBasketItemAsync(int id);
}
