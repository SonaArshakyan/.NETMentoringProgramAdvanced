using Catalog.Models;

namespace Catalog.Interfaces.Services;

public interface IMessagePublisher
{
    Task PublishProductMessageAsync(Product message);
}
