using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Security.Encryption;

namespace Persistence.Security.Converters;

/// <summary>
///     EF Core Value Converter that automatically encrypts and decrypts string properties.
/// </summary>
/// <remarks>
///     This converter intelligently detects whether data is already encrypted to prevent
///     double encryption. It uses Base64 validation to identify encrypted values.
///     Apply this converter to properties marked with the [Encrypted] attribute.
/// </remarks>
public class AesEncryptedConverter()
    : ValueConverter<string?, string?>(
        ToProviderExpression,
        FromProviderExpression)
{
    /// <summary>
    ///     Expression for converting values to database format (encryption).
    /// </summary>
    private static Expression<Func<string?, string?>> ToProviderExpression =>
        v => ToProvider(v);

    /// <summary>
    ///     Expression for converting database values to application format (decryption).
    /// </summary>
    private static Expression<Func<string?, string?>> FromProviderExpression =>
        v => FromProvider(v);

    /// <summary>
    ///     Validates if a string is in strict Base64 format.
    /// </summary>
    /// <param name="value">String to validate.</param>
    /// <returns>True if the string matches strict Base64 format.</returns>
    private static bool IsStrictBase64(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        // Check for valid Base64 characters and padding
        if (!Regex.IsMatch(value, @"^[A-Za-z0-9+/]*={0,2}$"))
            return false;

        // Base64 strings must be divisible by 4
        return value.Length % 4 == 0;
    }

    /// <summary>
    ///     Determines if a value appears to be already encrypted.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>True if the value looks like encrypted (Base64) data.</returns>
    private static bool LooksEncrypted(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IsStrictBase64(value))
            return false;

        try
        {
            // Attempt to decode Base64 to confirm validity
            _ = Convert.FromBase64String(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Converts plain text to encrypted format for database storage.
    ///     Prevents double encryption by checking if data is already encrypted.
    /// </summary>
    /// <param name="value">Plain text value.</param>
    /// <returns>Encrypted Base64 string.</returns>
    private static string? ToProvider(string? value)
    {
        // Don't encrypt if already encrypted or empty
        if (string.IsNullOrEmpty(value) || LooksEncrypted(value))
            return value;

        return AesEncryptionHelper.Encrypt(value);
    }

    /// <summary>
    ///     Converts encrypted database value back to plain text.
    ///     Returns original value if decryption fails or data is not encrypted.
    /// </summary>
    /// <param name="value">Encrypted Base64 string from database.</param>
    /// <returns>Decrypted plain text.</returns>
    private static string? FromProvider(string? value)
    {
        // Don't decrypt if not encrypted or empty
        if (string.IsNullOrEmpty(value) || !LooksEncrypted(value))
            return value;

        try
        {
            return AesEncryptionHelper.Decrypt(value);
        }
        catch
        {
            // Return original value if decryption fails
            return value;
        }
    }
}