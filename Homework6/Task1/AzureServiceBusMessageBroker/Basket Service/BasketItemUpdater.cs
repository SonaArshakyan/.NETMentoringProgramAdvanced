using AzureServiceBusMessageBroker.MessageModels;
using AzureServiceBusMessageBroker.Services;
using Basket.Interfaces.Services;
using Microsoft.ApplicationInsights;


namespace Basket;

public class BasketItemUpdater : BackgroundService
{
    private readonly IListnerService _listnerService;
    private readonly ILogger<BasketItemUpdater> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TelemetryClient _telemetryClient;

    public BasketItemUpdater(IListnerService listnerService,
                             ILogger<BasketItemUpdater> logger,
                             IServiceScopeFactory scopeFactory,
                             TelemetryClient telemetryClient)
    {
        _scopeFactory = scopeFactory;
        _listnerService = listnerService;
        _logger = logger;
        _telemetryClient = telemetryClient;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _listnerService.ListenMessagesAsync<BasketItemUpdateMessage>(HandleBasketItemUpdateMessage, HandleBasketItemUpdateErrorMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Eroor during message listening");
        }
    }
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await _listnerService.DisposeClientAsync();
    }

    private async void HandleBasketItemUpdateMessage(BasketItemUpdateMessage message)
    {

        using (var scope = _scopeFactory.CreateScope())
        {
            var basketService = scope.ServiceProvider.GetRequiredService<IBasketService>();

            var element = await basketService.GetByProductIdAsync(message.ProductId);
            if (element != null)
            {
                element.ProductName = message.ProductName;
                element.Price = message.Price; 
                await basketService.UpdateBasketItemAsync(element.Id, element);
            }
            var correlationId = message.ColorationId;
            _telemetryClient.TrackEvent("BasketItemUpdateMessage", new Dictionary<string, string>
            {
                 { "CorrelationId", correlationId }
            });
        }
    }
    private void HandleBasketItemUpdateErrorMessage(string message)
    {
        _logger.LogError(message);
    }
}
