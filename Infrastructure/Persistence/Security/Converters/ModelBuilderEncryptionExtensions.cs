using Domain.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Security.Converters;

public static class ModelBuilderEncryptionExtensions
{
    public static void ApplyEncryptedProperties(this ModelBuilder modelBuilder)
    {
        var converter = new AesEncryptedConverter();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            var encryptedProperties = clrType
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(EncryptedAttribute)));

            foreach (var prop in encryptedProperties)
            {
                modelBuilder
                    .Entity(clrType)
                    .Property(prop.Name)
                    .HasConversion(converter)
                    .HasColumnType("text");
            }
        }
    }
}