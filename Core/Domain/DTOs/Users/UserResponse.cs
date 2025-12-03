using Domain.Enums;

namespace Domain.DTOs.Users;

/// <summary>
///     Response model representing a user with decrypted sensitive data.
/// </summary>
/// <remarks>
///     Email and PhoneNumber are automatically decrypted from the database
///     and returned in plain text to the API consumer.
/// </remarks>
public class UserResponse
{
    /// <summary>
    ///     Unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     User's first name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     User's last name.
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    ///     User's email address (decrypted).
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     User's phone number (decrypted).
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     UTC timestamp when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     UTC timestamp when the user was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Current status of the user.
    /// </summary>
    public Status Status { get; set; }
}