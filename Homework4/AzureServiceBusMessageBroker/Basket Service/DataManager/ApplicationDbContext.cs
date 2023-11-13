using Basket.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.DataManager;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    DbSet<BasketItem> BasketItems { get; set; }

}
