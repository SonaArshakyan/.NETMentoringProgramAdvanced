using CartingService.DAL.Entities;
using LiteDB;

namespace CartingService.DAL.Data;

public class ApplicationDBContext: LiteDatabase
{
    public ApplicationDBContext(string dbPath) : base(dbPath)
    {
    }

    public ILiteCollection<Cart> Carts => GetCollection<Cart>("Carts");
}