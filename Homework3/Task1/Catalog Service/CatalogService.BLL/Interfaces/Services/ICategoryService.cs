using Application.Models;

namespace Application.Interfaces.Services;

public  interface ICategoryService
{
    Task<CategoryDTO> GetCategoryByIdAsync(int id);
    Task<List<CategoryDTO>> GetAllCategoriesAsync();
    Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category);
    Task UpdateCategoryAsync(int id, CategoryDTO category);
    Task DeleteCategoryAsync(int id);
}
