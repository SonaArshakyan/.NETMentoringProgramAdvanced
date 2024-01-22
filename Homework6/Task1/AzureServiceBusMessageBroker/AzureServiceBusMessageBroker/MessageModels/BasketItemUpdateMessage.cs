namespace AzureServiceBusMessageBroker.MessageModels;

public class BasketItemUpdateMessage
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string ColorationId { get; set; }
}
