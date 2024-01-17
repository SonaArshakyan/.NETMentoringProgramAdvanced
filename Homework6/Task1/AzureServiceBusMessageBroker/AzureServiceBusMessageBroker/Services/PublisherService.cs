using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace AzureServiceBusMessageBroker.Services;

public class PublisherService : IPublisherService
{
    private readonly AzureServiceBusClient _client;
    public PublisherService(AzureServiceBusClient azureServiceBusClient)
    {        
        _client = azureServiceBusClient;
    }

    public async Task PublishMessagesAsync<T>(T message)
    {
        ServiceBusSender sender = _client.ServiceBusClient.CreateSender(_client.TopicName);
        try
        {
            var messageBody = JsonConvert.SerializeObject(message);
            var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            await sender.SendMessageAsync(serviceBusMessage);
        }
        catch (Exception ex) {
            throw;
        }
        finally
        {
            await sender.DisposeAsync();
            await _client.DisposeAsync();
        }
    }
}
