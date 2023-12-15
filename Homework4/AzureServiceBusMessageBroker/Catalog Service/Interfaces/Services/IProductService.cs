using Catalog.Models;

namespace Catalog.Interfaces.Services;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);
    Task<List<Product>> GetAllProductsAsync(int categoryId);
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(int id, Product  Product);
    Task DeleteProductAsync(int id);
}
