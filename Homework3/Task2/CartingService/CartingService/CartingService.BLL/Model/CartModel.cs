using System.ComponentModel.DataAnnotations;

namespace CartingService.BLL.Model;

public class CartModel
{
    [Required]
    public string CartKey { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}