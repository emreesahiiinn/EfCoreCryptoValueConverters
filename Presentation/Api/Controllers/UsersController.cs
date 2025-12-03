using Application.Managers.Users;
using Domain.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
///     RESTful API controller for user management operations.
/// </summary>
/// <remarks>
///     This controller demonstrates automatic encryption/decryption of sensitive user data.
///     Email and PhoneNumber fields are transparently encrypted in the database and
///     decrypted when returned to the client.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserManager userManager) : ControllerBase
{
    /// <summary>
    ///     Creates a new user with encrypted sensitive data.
    /// </summary>
    /// <param name="request">User creation request containing user details.</param>
    /// <returns>Created user with decrypted data.</returns>
    /// <response code="201">User successfully created.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
    {
        var response = await userManager.CreateUserAsync(request);
        return CreatedAtAction(nameof(GetUser), new { id = response.Id }, response);
    }

    /// <summary>
    ///     Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the user.</param>
    /// <returns>User details with decrypted sensitive data.</returns>
    /// <response code="200">User found and returned.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetUser(Guid id)
    {
        var user = await userManager.GetUserByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    ///     Retrieves all users from the database.
    /// </summary>
    /// <returns>List of all users with decrypted sensitive data.</returns>
    /// <response code="200">Users successfully retrieved.</response>
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
    {
        var users = await userManager.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    ///     Updates an existing user's information.
    /// </summary>
    /// <param name="request">User update request with modified details.</param>
    /// <returns>Updated user details with decrypted data.</returns>
    /// <response code="200">User successfully updated.</response>
    /// <response code="404">User not found.</response>
    [HttpPut]
    public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        try
        {
            var response = await userManager.UpdateUserAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    ///     Deletes a user from the database.
    /// </summary>
    /// <param name="id">Unique identifier of the user to delete.</param>
    /// <returns>No content on successful deletion.</returns>
    /// <response code="204">User successfully deleted.</response>
    /// <response code="404">User not found.</response>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await userManager.DeleteUserAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}