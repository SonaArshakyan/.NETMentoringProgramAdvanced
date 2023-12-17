namespace Catalog.Models;

public class Category
{
    public Category()
    {
        Products = new List<Product>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    List<Product> Products { get; set; }
}
