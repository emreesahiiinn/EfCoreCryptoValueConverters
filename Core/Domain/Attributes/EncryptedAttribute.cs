namespace Domain.Attributes;

/// <summary>
///     Marks a property for automatic encryption/decryption in the database.
///     Properties decorated with this attribute will be transparently encrypted
///     using AES encryption before being stored and decrypted when retrieved.
/// </summary>
/// <remarks>
///     This attribute is processed by ModelBuilderEncryptionExtensions during
///     EF Core model configuration to apply the AesEncryptedConverter.
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class EncryptedAttribute : Attribute
{
}