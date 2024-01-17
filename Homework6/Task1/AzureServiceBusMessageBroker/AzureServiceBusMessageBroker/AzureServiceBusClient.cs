using Azure.Messaging.ServiceBus;

namespace AzureServiceBusMessageBroker;

public class AzureServiceBusClient
{
    private readonly string _serviceBusConnectionString;
    private readonly string _topicName;
    private readonly string _subscriptionName;
    private ServiceBusClient srviceBusClient;

    public AzureServiceBusClient(string serviceBusConnectionString, string topicName, string subscriptionName)
    {
        _serviceBusConnectionString = serviceBusConnectionString;
        _topicName = topicName;
        _subscriptionName = subscriptionName;
    }

    public string TopicName { get { return _topicName; } }

    public string SubscriptionName { get { return _subscriptionName; } }

    public ServiceBusClient ServiceBusClient
    {
        get
        {
            if (srviceBusClient == null)
            {
                srviceBusClient = new ServiceBusClient(_serviceBusConnectionString);
            }
            return srviceBusClient;
        }
    }
    public async Task DisposeAsync()
    {
        await srviceBusClient.DisposeAsync();
        srviceBusClient = null;
    }
}
