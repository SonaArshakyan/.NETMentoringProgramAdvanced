using AzureServiceBusMessageBroker.Services;
using AzureServiceBusMessageBroker.MessageModels;
using Catalog.Models;

namespace Catalog.Interfaces.Services;

public class ProductMessagePublisher : IMessagePublisher
{
    private readonly IPublisherService _publisherService;
    private readonly ILogger<ProductMessagePublisher> _logger;
    public ProductMessagePublisher(IPublisherService publisherService, ILogger<ProductMessagePublisher> logger)
    {
        _publisherService = publisherService;
        _logger = logger;
    }
    public async Task PublishProductMessageAsync(Product product)
    {
        try
        {
            BasketItemUpdateMessage basketItem = new BasketItemUpdateMessage()
            {
                 Price = product.Price,
                 ProductName = product.Name,
                 ProductId = product.Id,
            };
             
            await _publisherService.PublishMessagesAsync<BasketItemUpdateMessage>(basketItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
