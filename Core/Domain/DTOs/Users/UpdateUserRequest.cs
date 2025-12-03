namespace Domain.DTOs.Users;

/// <summary>
///     Request model for updating an existing user.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    ///     Unique identifier of the user to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Updated first name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Updated last name.
    /// </summary>
    public required string Surname { get; set; }

    /// <summary>
    ///     Updated email address. Will be encrypted in the database.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    ///     Updated phone number. Will be encrypted in the database.
    /// </summary>
    public required string PhoneNumber { get; set; }
}