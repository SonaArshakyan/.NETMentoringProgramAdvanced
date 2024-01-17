using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace AzureServiceBusMessageBroker.Services;

public class ListnerService : IListnerService
{
    private readonly AzureServiceBusClient _client;

    public ListnerService(AzureServiceBusClient azureServiceBusClient)
    {
        _client = azureServiceBusClient;
    }
    public async Task ListenMessagesAsync<T>(Action<T> handleMessage, Action<string> handleError)
    {
        var processorOptions = new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 1,
            AutoCompleteMessages = false
        };
        ServiceBusProcessor processor = _client.ServiceBusClient
                                 .CreateProcessor(_client.TopicName, _client.SubscriptionName, processorOptions);

            processor.ProcessMessageAsync += async args =>
            {
                await MessageHandler<T>(args, handleMessage);
            };

            processor.ProcessErrorAsync += async args =>
            {
                await ErrorHandler<T>(args, handleError);
            };

            await processor.StartProcessingAsync();
    }

    async Task MessageHandler<T>(ProcessMessageEventArgs args, Action<T> handleMessage)
    {
        var messageBody = Encoding.UTF8.GetString(args.Message.Body);
        var message = JsonConvert.DeserializeObject<T>(messageBody);

        handleMessage?.Invoke(message);

        await args.CompleteMessageAsync(args.Message);
    }

    Task ErrorHandler<T>(ProcessErrorEventArgs args, Action<string> handleError)
    {
        handleError?.Invoke(args.Exception.Message);

        return Task.CompletedTask;
    }

    public async Task DisposeClientAsync()
    {
        await _client.DisposeAsync();
    }
}
