using Application.Models;

namespace Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<List<ProductDTO>> GetAllProductsAsync();
    Task<ProductDTO> CreateProductAsync(ProductDTO product);
    Task UpdateProductAsync(int id, ProductDTO  ProductDTO);
    Task DeleteProductAsync(int id);
}
