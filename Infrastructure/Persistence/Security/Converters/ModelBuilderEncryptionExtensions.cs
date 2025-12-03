using Domain.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Security.Converters;

/// <summary>
///     Extension methods for ModelBuilder to automatically apply encryption converters.
/// </summary>
public static class ModelBuilderEncryptionExtensions
{
    /// <summary>
    ///     Scans all entities in the model and applies AES encryption converter
    ///     to properties marked with the [Encrypted] attribute.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance.</param>
    /// <remarks>
    ///     This method should be called in DbContext.OnModelCreating to enable
    ///     automatic encryption/decryption for all marked properties.
    /// </remarks>
    public static void ApplyEncryptedProperties(this ModelBuilder modelBuilder)
    {
        var converter = new AesEncryptedConverter();

        // Iterate through all entity types in the model
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            // Find all properties marked with [Encrypted] attribute
            var encryptedProperties = clrType
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(EncryptedAttribute)));

            // Apply the encryption converter to each marked property
            foreach (var prop in encryptedProperties)
                modelBuilder
                    .Entity(clrType)
                    .Property(prop.Name)
                    .HasConversion(converter)
                    .HasColumnType("text"); // Store as text in PostgreSQL
        }
    }
}