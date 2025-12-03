using Application.Managers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyRegistration;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManager, UserManager>();
    }
}
