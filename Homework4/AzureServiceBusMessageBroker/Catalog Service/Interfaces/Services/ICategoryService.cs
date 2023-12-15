using Catalog.Models;

namespace Catalog.Interfaces.Services;

public  interface ICategoryService
{
    Task<Category> GetCategoryByIdAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(int id, Category category);
    Task DeleteCategoryAsync(int id);
}
