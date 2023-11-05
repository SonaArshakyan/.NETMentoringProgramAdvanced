using Application.Common;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.Models;
using Domain;

namespace Application.Interfaces.Services;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> repository;
    private readonly IMapper mapper;

    public ProductService(IGenericRepository<Product> genericRepository, IMapper mapper)
    {
        repository = genericRepository;
        this.mapper = mapper;
    }
    public async Task<ProductDTO> CreateProductAsync(ProductDTO product)
    {
        var result = await repository.AddAsync(mapper.Map<Product>(product));
        return mapper.Map<ProductDTO>(result);
    }

    public async Task DeleteProductAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<List<ProductDTO>> GetAllProductsAsync()
    {
        var result = await repository.GetAllAsync();
        return result.Select(p => mapper.Map<ProductDTO>(p)).ToList();
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var result = await repository.GetByIdAsync(id);
        return mapper.Map<ProductDTO>(result);
    }

    public async Task UpdateProductAsync(int id, ProductDTO product)
    {
        var entity = mapper.Map<Product>(product);
        entity.Id = id;
       await repository.UpdateAsync(mapper.Map<Product>(product));
    }
}
