using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models;

public class CategoryDTO
{
    public CategoryDTO()
    {
        Products = new List<ProductDTO>();
    }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    public string Image { get; set; }
    List<ProductDTO> Products { get; set; }
}