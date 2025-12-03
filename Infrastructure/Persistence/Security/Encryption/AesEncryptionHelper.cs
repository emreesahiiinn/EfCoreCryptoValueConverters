using System.Security.Cryptography;
using System.Text;

namespace Persistence.Security.Encryption;

/// <summary>
///     Provides AES encryption and decryption functionality for sensitive data.
/// </summary>
/// <remarks>
///     WARNING: This implementation uses hardcoded keys for demonstration purposes only.
///     In production, keys should be stored securely in a key vault (Azure Key Vault, AWS KMS, etc.)
///     and rotated regularly. Each encryption should use a unique IV for maximum security.
/// </remarks>
public static class AesEncryptionHelper
{
    // WARNING: Never hardcode encryption keys in production!
    // Use environment variables, Azure Key Vault, or AWS Secrets Manager instead.
    private static readonly byte[] AesKey = "emreesahiiinnaes"u8.ToArray();
    private static readonly byte[] AesIv = "aesemreesahiiinn"u8.ToArray();

    /// <summary>
    ///     Encrypts plain text using AES-256 encryption in CBC mode.
    /// </summary>
    /// <param name="plainText">The text to encrypt.</param>
    /// <returns>Base64-encoded encrypted string.</returns>
    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        using var aes = Aes.Create();
        aes.Key = AesKey;
        aes.IV = AesIv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    ///     Decrypts AES-encrypted text that was encrypted using the Encrypt method.
    /// </summary>
    /// <param name="cipherText">Base64-encoded encrypted string.</param>
    /// <returns>Decrypted plain text.</returns>
    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        var encryptedBytes = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = AesKey;
        aes.IV = AesIv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}