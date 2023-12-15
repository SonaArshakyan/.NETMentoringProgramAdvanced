namespace AzureServiceBusMessageBroker.Services;

public interface IListnerService
{
    Task ListenMessagesAsync<T>(Action<T> handleMessage, Action<string> handleError);
    Task DisposeClientAsync();
}
