using Domain.Attributes;
using Domain.Entities.Common;

namespace Domain.Entities;

/// <summary>
///     Represents a user entity with encrypted sensitive information.
/// </summary>
/// <remarks>
///     Email and PhoneNumber properties are automatically encrypted in the database
///     using AES encryption through the [Encrypted] attribute.
/// </remarks>
public class User : BaseEntity
{
    /// <summary>
    ///     User's first name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     User's last name.
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    ///     User's email address. This field is encrypted in the database.
    /// </summary>
    [Encrypted]
    public string Email { get; set; }

    /// <summary>
    ///     User's phone number. This field is encrypted in the database.
    /// </summary>
    [Encrypted]
    public string PhoneNumber { get; set; }
}