using Application.Models;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain;

namespace Application.Interfaces.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> repository;
    private readonly IMapper mapper;
    public CategoryService( IGenericRepository<Category> genericRepository , IMapper mapper)
    {
        repository = genericRepository;
        this.mapper = mapper;
    }
    public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category)
    {
        var result = await repository.AddAsync(mapper.Map<Category>(category));
        return mapper.Map<CategoryDTO>(result);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
    {
        var result = await repository.GetAllAsync();
        return result.Select(p => mapper.Map<CategoryDTO>(p)).ToList();
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
    {
        var result = await repository.GetByIdAsync(id);
        return mapper.Map<CategoryDTO>(result);
    }

    public async Task UpdateCategoryAsync(int id, CategoryDTO category)
    {
        var entity = mapper.Map<Category>(category);
        entity.Id = id;
        await repository.UpdateAsync(entity);
    }
}
