using Domain.DTOs.Users;

namespace Application.Managers.Users;

/// <summary>
///     User manager interface defining business operations for user management.
/// </summary>
public interface IUserManager
{
    /// <summary>
    ///     Creates a new user with encrypted sensitive data.
    /// </summary>
    /// <param name="request">User creation request containing user details.</param>
    /// <returns>Created user details with decrypted data.</returns>
    Task<UserResponse> CreateUserAsync(CreateUserRequest request);

    /// <summary>
    ///     Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the user.</param>
    /// <returns>User details with decrypted data, or null if not found.</returns>
    Task<UserResponse?> GetUserByIdAsync(Guid id);

    /// <summary>
    ///     Retrieves all users from the database.
    /// </summary>
    /// <returns>List of all users with decrypted data.</returns>
    Task<List<UserResponse>> GetAllUsersAsync();

    /// <summary>
    ///     Updates an existing user's information.
    /// </summary>
    /// <param name="request">User update request containing updated details.</param>
    /// <returns>Updated user details with decrypted data.</returns>
    /// <exception cref="Exception">Thrown when user is not found.</exception>
    Task<UserResponse> UpdateUserAsync(UpdateUserRequest request);

    /// <summary>
    ///     Deletes a user from the database.
    /// </summary>
    /// <param name="id">Unique identifier of the user to delete.</param>
    /// <returns>True if user was deleted, false if not found.</returns>
    Task<bool> DeleteUserAsync(Guid id);
}