using System.Security.Cryptography;
using System.Text;

namespace Persistence.Security.Encryption;

public static class AesEncryptionHelper
{
    private static readonly byte[] AesKey = "emreesahiiinnaes"u8.ToArray();
    private static readonly byte[] AesIv = "aesemreesahiiinn"u8.ToArray();

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