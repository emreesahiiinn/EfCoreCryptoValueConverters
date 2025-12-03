using Domain.DTOs.Users;

namespace Application.Managers.Users;

public interface IUserManager
{
    Task<UserResponse> CreateUserAsync(CreateUserRequest request);
    Task<UserResponse?> GetUserByIdAsync(Guid id);
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<UserResponse> UpdateUserAsync(UpdateUserRequest request);
    Task<bool> DeleteUserAsync(Guid id);
}
