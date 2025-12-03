using Microsoft.Extensions.Configuration;

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

            return configurationManager.GetConnectionString("Psql");
        }
    }
}