using Basket.Models;

namespace Basket.Interfaces.Services;

public interface IBasketItemUpdater
{
    Task<List<BasketItem>> UpdateBasketItemsAsync();
}
