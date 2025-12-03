using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.Contexts;

public class EfCoreCryptoValueConvertersContextFactory : IDesignTimeDbContextFactory<EfCoreCryptoValueConvertersContext>
{
    public EfCoreCryptoValueConvertersContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfCoreCryptoValueConvertersContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=EfCoreCryptoDb;Username=postgres;Password=postgres");

        return new EfCoreCryptoValueConvertersContext(optionsBuilder.Options);
    }
}
