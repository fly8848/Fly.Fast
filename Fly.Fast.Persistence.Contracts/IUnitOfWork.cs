namespace Fly.Fast.Persistence.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(Action<IServiceProvider>? before = null, Action<IServiceProvider>? after = null,
        CancellationToken cancellationToken = default);

    Task CommitAsync(Action<IServiceProvider>? before = null, Action<IServiceProvider>? after = null,
        CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}