using Catalog.Interfaces.Repositories;
using Catalog.Models;

namespace Catalog.Interfaces.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> repository;
    private readonly IProductService productService;
    public CategoryService( IGenericRepository<Category> genericRepository ,IProductService productService)
    {
        repository = genericRepository;
        this.productService = productService;
    }
    public async Task<Category> CreateCategoryAsync(Category category)
    {
        return await repository.AddAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var relatedItems = await productService.GetAllProductsAsync(id);
        foreach (var item in relatedItems)
        {
            await productService.DeleteProductAsync(item.Id);
        }
        await repository.DeleteAsync(id);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
        category.Id = id;
        await repository.UpdateAsync(id, category);
    }
}
