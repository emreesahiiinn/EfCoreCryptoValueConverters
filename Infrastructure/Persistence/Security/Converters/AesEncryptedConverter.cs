using Persistence.Security.Encryption;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Persistence.Security.Converters;

public class AesEncryptedConverter()
    : ValueConverter<string?, string?>(
        ToProviderExpression,
        FromProviderExpression)
{
    private static bool IsStrictBase64(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!Regex.IsMatch(value, @"^[A-Za-z0-9+/]*={0,2}$"))
            return false;

        return value.Length % 4 == 0;
    }

    private static bool LooksEncrypted(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IsStrictBase64(value))
            return false;

        try
        {
            _ = Convert.FromBase64String(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string? ToProvider(string? value)
    {
        if (string.IsNullOrEmpty(value) || LooksEncrypted(value))
            return value;

        return AesEncryptionHelper.Encrypt(value);
    }

    private static string? FromProvider(string? value)
    {
        if (string.IsNullOrEmpty(value) || !LooksEncrypted(value))
            return value;

        try
        {
            return AesEncryptionHelper.Decrypt(value);
        }
        catch
        {
            return value;
        }
    }

    private static Expression<Func<string?, string?>> ToProviderExpression =>
        v => ToProvider(v);

    private static Expression<Func<string?, string?>> FromProviderExpression =>
        v => FromProvider(v);
}