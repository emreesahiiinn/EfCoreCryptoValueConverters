using Application.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Configurations;
using Persistence.Contexts;
using Persistence.Repositories.Users;

namespace Persistence.DependencyRegistration;

/// <summary>
///     Persistence layer dependency injection configuration.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    ///     Registers persistence layer services (DbContext, repositories) to the DI container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        // Register DbContext with PostgreSQL provider
        services.AddDbContext<EfCoreCryptoValueConvertersContext>(option =>
            option.UseNpgsql(Configuration.ConfigurationString));

        // Register repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
    }
}