namespace Fly.Fast.Persistence.Contracts;

public class UnitOfWorkOptions
{
    public Action<IServiceProvider>? BeforeSaveChanges { get; set; }
    public Action<IServiceProvider>? AfterSaveChanges { get; set; }
    public Action<IServiceProvider>? BeforeCommit { get; set; }
    public Action<IServiceProvider>? AfterCommit { get; set; }
}