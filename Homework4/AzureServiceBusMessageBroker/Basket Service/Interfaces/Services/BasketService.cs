using Basket.Interfaces.Repositories;
using Basket.Models;

namespace Basket.Interfaces.Services;

public class BasketService : IBasketService
{
    private readonly IGenericRepository<BasketItem> repository;
    public BasketService(IGenericRepository<BasketItem> genericRepository)
    {
        repository = genericRepository;
    }
    public async Task<BasketItem> AddItemInBasketAsync(BasketItem item)
    {
        return await repository.AddAsync(item);
    }

    public async Task DeleteBasketItemAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<List<BasketItem>> GetAllItemsAstnc()
    {
        return await repository.GetAllAsync();
    }

    public async Task UpdateBasketItemAsync(int id, BasketItem item)
    {
        item.Id = id;
        await repository.UpdateAsync(id, item);
    }
}
