using Application.Managers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyRegistration;

/// <summary>
///     Application layer dependency injection configuration.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    ///     Registers application layer services (managers, business logic) to the DI container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
    }
}