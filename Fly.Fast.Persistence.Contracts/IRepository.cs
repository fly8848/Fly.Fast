using System.Linq.Expressions;

namespace Fly.Fast.Persistence.Contracts;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> UpdateRangeAsync(List<T> entities);
    Task<T> DeleteAsync(T entity);
    Task<List<T>> DeleteRangeAsync(List<T> entities);
    Task<T?> FindAsync(object id, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}