using System.ComponentModel.DataAnnotations;

namespace Catalog_Service;

public class ProductDTO
{
    [Required]
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public string Image { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Amount { get; set; }
}
