namespace AzureServiceBusMessageBroker.MessageModels;

public class BasketItemUpdateMessage
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
}
