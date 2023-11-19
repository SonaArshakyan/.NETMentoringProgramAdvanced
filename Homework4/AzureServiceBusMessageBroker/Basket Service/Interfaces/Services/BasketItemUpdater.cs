using AzureServiceBusMessageBroker.MessageModels;
using AzureServiceBusMessageBroker.Services;
using Basket.Interfaces.Repositories;
using Basket.Models;

namespace Basket.Interfaces.Services;

public class BasketItemUpdater : IBasketItemUpdater
{
    private readonly IListnerService _listnerService;
    private readonly ILogger<BasketItemUpdater> _logger;
    private readonly IGenericRepository<BasketItem> _repository;
    private List<BasketItem> basketItems;

    public BasketItemUpdater(IListnerService listnerService, IGenericRepository<BasketItem> genericRepository, ILogger<BasketItemUpdater> logger)
    {
        _listnerService = listnerService;
        _repository = genericRepository;
        _logger = logger;
    }
    public async Task<List<BasketItem>> UpdateBasketItemsAsync()
    {
        try
        {
            basketItems = await _repository.GetAllAsync();
            await _listnerService.ListenMessagesAsync<BasketItemUpdateMessage>(HandleBasketItemUpdateMessage, HandleBasketItemUpdateErrorMessage);
            _repository.AddRange(basketItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return await Task.FromResult(basketItems);
    }
    private void HandleBasketItemUpdateMessage(BasketItemUpdateMessage message)
    {
        var element = basketItems.Where(p => p.ProductId == message.ProductId).FirstOrDefault();
        if (element != null)
        {
            element.ProductName = message.ProductName;
            element.Price = message.Price;
        }
    }
    private void HandleBasketItemUpdateErrorMessage(string message)
    {
        _logger.LogError(message);
    }
}
