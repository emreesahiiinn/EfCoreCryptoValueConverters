using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.Contexts;

/// <summary>
///     Design-time factory for creating DbContext instances during EF Core migrations.
/// </summary>
/// <remarks>
///     This factory is used by EF Core tools (dotnet ef) at design time to create
///     DbContext instances for migrations, database updates, and scaffolding operations.
///     The connection string is hardcoded here for development purposes only.
/// </remarks>
public class EfCoreCryptoValueConvertersContextFactory : IDesignTimeDbContextFactory<EfCoreCryptoValueConvertersContext>
{
    /// <summary>
    ///     Creates a new instance of the DbContext for design-time operations.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the EF Core tools.</param>
    /// <returns>Configured DbContext instance.</returns>
    public EfCoreCryptoValueConvertersContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfCoreCryptoValueConvertersContext>();

        // Connection string for migrations - update with your PostgreSQL credentials
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=EfCoreCryptoDb;Username=postgres;Password=postgres");

        return new EfCoreCryptoValueConvertersContext(optionsBuilder.Options);
    }
}