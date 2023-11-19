namespace AzureServiceBusMessageBroker.Services;

public interface IPublisherService
{
    Task PublishMessagesAsync<T>(T message);
}
