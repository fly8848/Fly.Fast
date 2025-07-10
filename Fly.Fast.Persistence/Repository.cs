using System.Linq.Expressions;
using Fly.Fast.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fly.Fast.Persistence;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    protected Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public Task<T> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public Task<List<T>> UpdateRangeAsync(List<T> entities)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        return Task.FromResult(entities);
    }

    public Task<T> DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.FromResult(entity);
    }

    public Task<List<T>> DeleteRangeAsync(List<T> entities)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        return Task.FromResult(entities);
    }

    public async Task<T?> FindAsync(object id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().SingleOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().CountAsync(expression, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AnyAsync(expression, cancellationToken);
    }
}