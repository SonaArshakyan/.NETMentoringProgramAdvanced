using Microsoft.EntityFrameworkCore;

namespace Basket.DataManager;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    DbSet<Models.Basket> Basket { get; set; }

}
