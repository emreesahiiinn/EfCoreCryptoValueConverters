using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Security.Converters;

namespace Persistence.Contexts;

/// <summary>
///     Database context for the EF Core Crypto Value Converters demonstration.
/// </summary>
/// <remarks>
///     This context automatically applies encryption converters to properties
///     marked with [Encrypted] attribute and manages audit timestamps for entities.
/// </remarks>
public class EfCoreCryptoValueConvertersContext(DbContextOptions options)
    : DbContext(options)
{
    /// <summary>
    ///     DbSet for User entities.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    ///     Configures the model and applies encryption converters.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply encryption converters to all [Encrypted] properties
        modelBuilder.ApplyEncryptedProperties();
    }

    /// <summary>
    ///     Overrides SaveChangesAsync to automatically set audit timestamps.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var data = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in data)
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}