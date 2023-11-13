using Catalog.Interfaces.Repositories;
using Catalog.Models;

namespace Catalog.Interfaces.Services;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> repository;

    public ProductService(IGenericRepository<Product> genericRepository)
    {
        repository = genericRepository;
    }
    public async Task<Product> CreateProductAsync(Product product)
    {
        return await repository.AddAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<List<Product>> GetAllProductsAsync(int categoryId)
    {
        return (await repository.GetAllAsync())
                                       .Where(c => c.CategoryId == categoryId).ToList();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task UpdateProductAsync(int id, Product product)
    {
        product.Id = id;
        await repository.UpdateAsync(id, product);
    }
}
