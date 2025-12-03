using Microsoft.Extensions.Configuration;

namespace Persistence.Configurations;

/// <summary>
///     Database configuration helper for reading connection strings.
/// </summary>
internal static class Configuration
{
    /// <summary>
    ///     Gets the PostgreSQL connection string from appsettings.json.
    /// </summary>
    /// <remarks>
    ///     This configuration reads from the API project's appsettings.json file.
    ///     In production, consider using environment variables or secure configuration providers.
    /// </remarks>
    public static string? ConfigurationString
    {
        get
        {
            ConfigurationManager configurationManager = new();

            // Navigate to API project folder to read appsettings.json
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                "../../Presentation/Api"));
            configurationManager.AddJsonFile("appsettings.json");

            return configurationManager.GetConnectionString("Psql");
        }
    }
}