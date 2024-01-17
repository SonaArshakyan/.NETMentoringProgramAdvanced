using AzureServiceBusMessageBroker.Services;
using AzureServiceBusMessageBroker.MessageModels;
using Catalog.Models;
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace Catalog.Interfaces.Services;

public class ProductMessagePublisher : IMessagePublisher
{
    private readonly IPublisherService _publisherService;
    private readonly ILogger<ProductMessagePublisher> _logger;
    private readonly TelemetryClient _telemetryClient;

    public ProductMessagePublisher(IPublisherService publisherService, ILogger<ProductMessagePublisher> logger, TelemetryClient telemetryClient)
    {
        _publisherService = publisherService;
        _logger = logger;
        _telemetryClient = telemetryClient;
    }
    public async Task PublishProductMessageAsync(Product product)
    {
        var correlationId = Activity.Current?.TraceId.ToString();
        try
        {
            _telemetryClient.TrackEvent("PublishProductMessageAsync", new Dictionary<string, string>
            {
            { "CorrelationId", correlationId }
            });
            BasketItemUpdateMessage basketItem = new BasketItemUpdateMessage()
            {
                 Price = product.Price,
                 ProductName = product.Name,
                 ProductId = product.Id,
                 ColorationId = correlationId
            };
             
            await _publisherService.PublishMessagesAsync<BasketItemUpdateMessage>(basketItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _telemetryClient.TrackException(ex, new Dictionary<string, string>
            {
            { "CorrelationId", correlationId }
            });
        }
    }
}
