namespace Domain.DTOs.Users;

/// <summary>
///     Request model for creating a new user.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    ///     User's first name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     User's last name.
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    ///     User's email address. Will be encrypted in the database.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    ///     User's phone number. Will be encrypted in the database.
    /// </summary>
    public required string PhoneNumber { get; set; }
}