namespace Fly.Fast.Persistence.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(Func<IServiceProvider, Task>? before = null, Func<IServiceProvider, Task>? after = null,
        CancellationToken cancellationToken = default);

    Task CommitAsync(Func<IServiceProvider, Task>? before = null, Func<IServiceProvider, Task>? after = null,
        CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}