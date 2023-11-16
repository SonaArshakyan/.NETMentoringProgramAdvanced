using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Models;

public class ProductDTO
{
    [JsonIgnore]
    public int Id { get; set; }

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
