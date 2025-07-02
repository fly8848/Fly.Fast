using Fly.Fast.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fly.Fast.Persistence.EntityFrameworkCore;

public static class DependencyInjection
{
    public static void AddUnitOfWork<T>(this IServiceCollection services, Action<UnitOfWorkOptions>? action = null)
        where T : DbContext
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

        var unitOfWorkOptions = new UnitOfWorkOptions();
        action?.Invoke(unitOfWorkOptions);

        services.AddScoped<UnitOfWorkOptions>(_ => unitOfWorkOptions);
    }
}