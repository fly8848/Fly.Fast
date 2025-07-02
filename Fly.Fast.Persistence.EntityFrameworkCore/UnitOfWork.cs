using Fly.Fast.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Fly.Fast.Persistence.EntityFrameworkCore;

public class UnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _dbContext;
    private readonly ILogger<UnitOfWork<T>> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly UnitOfWorkOptions _unitOfWorkOptions;

    private IDbContextTransaction? _transaction;

    public UnitOfWork(
        T dbContext,
        IServiceProvider serviceProvider,
        UnitOfWorkOptions unitOfWorkOptions,
        ILogger<UnitOfWork<T>> logger
    )
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
        _unitOfWorkOptions = unitOfWorkOptions;
        _logger = logger;
    }

    public async Task SaveChangesAsync(Action<IServiceProvider>? before = null, Action<IServiceProvider>? after = null,
        CancellationToken cancellationToken = default)
    {
        if (before != null) _unitOfWorkOptions.BeforeSaveChanges = before;

        if (after != null) _unitOfWorkOptions.AfterSaveChanges = after;

        _unitOfWorkOptions.BeforeSaveChanges?.Invoke(_serviceProvider);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _unitOfWorkOptions.AfterSaveChanges?.Invoke(_serviceProvider);
    }


    public async Task CommitAsync(Action<IServiceProvider>? before = null, Action<IServiceProvider>? after = null,
        CancellationToken cancellationToken = default)
    {
        if (_transaction == null) throw new ArgumentNullException(nameof(_transaction));

        if (before != null) _unitOfWorkOptions.BeforeCommit = before;

        if (after != null) _unitOfWorkOptions.AfterCommit = after;

        _unitOfWorkOptions.BeforeCommit?.Invoke(_serviceProvider);

        await _transaction.CommitAsync(cancellationToken);

        _unitOfWorkOptions.AfterCommit?.Invoke(_serviceProvider);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) throw new ArgumentNullException(nameof(_transaction));

        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) throw new ArgumentNullException(nameof(_transaction));

        await _transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        _logger.LogDebug("{name}: Disposing...", nameof(UnitOfWork<T>));
        _transaction?.Dispose();
        _transaction = null;
    }
}