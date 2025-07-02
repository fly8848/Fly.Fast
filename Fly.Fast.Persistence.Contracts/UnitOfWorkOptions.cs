namespace Fly.Fast.Persistence.Contracts;

public class UnitOfWorkOptions
{
    public Func<IServiceProvider, Task>? BeforeSaveChangesAsync { get; set; }
    public Func<IServiceProvider, Task>? AfterSaveChangesAsync { get; set; }
    public Func<IServiceProvider, Task>? BeforeCommitAsync { get; set; }
    public Func<IServiceProvider, Task>? AfterCommitAsync { get; set; }
}