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

    public async Task SaveChangesAsync(bool isExecuteBefore = true, bool isExecuteAfter = true,
        CancellationToken cancellationToken = default)
    {
        if (isExecuteBefore) _unitOfWorkOptions.BeforeSaveChangesAsync?.Invoke(_serviceProvider);

        await _dbContext.SaveChangesAsync(cancellationToken);

        if (isExecuteAfter) _unitOfWorkOptions.AfterSaveChangesAsync?.Invoke(_serviceProvider);
    }

    public async Task CommitAsync(bool isExecuteBefore = true, bool isExecuteAfter = true,
        CancellationToken cancellationToken = default)
    {
        if (_transaction == null) throw new ArgumentNullException(nameof(_transaction));

        if (isExecuteBefore) _unitOfWorkOptions.BeforeCommitAsync?.Invoke(_serviceProvider);

        await _transaction.CommitAsync(cancellationToken);

        if (isExecuteAfter) _unitOfWorkOptions.AfterCommitAsync?.Invoke(_serviceProvider);
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
        _transaction?.Dispose();
        _transaction = null;
        _logger.LogDebug("{name}: Disposed...", nameof(UnitOfWork<T>));
    }
}