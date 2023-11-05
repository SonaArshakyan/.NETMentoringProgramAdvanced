using Domain.Entities;

namespace Domain;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
}
