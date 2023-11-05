using Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.EFMsSql;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    DbSet<Product> Product { get; set; }

    DbSet<Category> Category { get; set; }

}
