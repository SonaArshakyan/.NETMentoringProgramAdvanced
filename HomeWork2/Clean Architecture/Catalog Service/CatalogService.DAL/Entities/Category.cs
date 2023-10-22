using Domain.Entities;

namespace Domain;

public class Category : BaseEntity
{
    public Category()
    {
        Products = new List<Product>();
    }
    public string Name { get; set; }
    public string Image { get; set; }
    List<Product> Products { get; set; }
}
