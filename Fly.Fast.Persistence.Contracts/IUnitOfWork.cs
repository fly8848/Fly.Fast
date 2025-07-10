namespace Fly.Fast.Persistence.Contracts;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(bool isExecuteBefore = true, bool isExecuteAfter = true,
        CancellationToken cancellationToken = default);

    Task CommitAsync(bool isExecuteBefore = true, bool isExecuteAfter = true,
        CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}