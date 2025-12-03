using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Persistence.Configurations;

static class Configuration
{
    public static string? ConfigurationString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "../../Presentation/Api"));
            configurationManager.AddJsonFile("appsettings.json");

            DbContextOptionsBuilder<EfCoreCryptoValueConvertersContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(configurationManager.GetConnectionString("Psql"));

            return configurationManager.GetConnectionString("Psql");
        }
    }
}