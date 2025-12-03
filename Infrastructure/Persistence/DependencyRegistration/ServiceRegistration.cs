using Application.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Configurations;
using Persistence.Contexts;
using Persistence.Repositories.Users;

namespace Persistence.DependencyRegistration;


public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<EfCoreCryptoValueConvertersContext>(option =>
            option.UseNpgsql(Configuration.ConfigurationString));

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
    }
}