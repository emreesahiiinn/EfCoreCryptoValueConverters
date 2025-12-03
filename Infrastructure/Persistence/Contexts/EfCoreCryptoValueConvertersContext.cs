using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Security.Converters;

namespace Persistence.Contexts;

public class EfCoreCryptoValueConvertersContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyEncryptedProperties();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var data = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in data)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}