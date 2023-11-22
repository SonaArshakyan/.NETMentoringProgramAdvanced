using Basket.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Basket.DataManager.Repositories;

public class EFGenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    public EFGenericRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<T> AddAsync(T entity)
    {
        try
        {
            await _dbContext.Set<T>().AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public Task UpdateAsync(int id, T entity)
    {
        T exist = _dbContext.Set<T>().Find(id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        _dbContext.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _dbContext.Set<T>().Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext
            .Set<T>()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public void UpdateRange(List<T> values)
    {
        _dbContext.Set<T>().UpdateRange(values);
    }
}

