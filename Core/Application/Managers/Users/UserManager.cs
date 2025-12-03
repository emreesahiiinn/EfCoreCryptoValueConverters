using Application.Repositories.Users;
using Domain.DTOs.Users;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Managers.Users;

public class UserManager(IUserReadRepository readRepository, IUserWriteRepository writeRepository) : IUserManager
{
    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Status = Status.Active
        };

        await writeRepository.AddAsync(user);
        await writeRepository.SaveAsync();

        return MapToResponse(user);
    }

    public async Task<UserResponse?> GetUserByIdAsync(Guid id)
    {
        var user = await readRepository.GetByIdAsync(id.ToString(), tracking: false);
        return user == null ? null : MapToResponse(user);
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await readRepository.GetAll(tracking: false).ToListAsync();
        return users.Select(MapToResponse).ToList();
    }

    public async Task<UserResponse> UpdateUserAsync(UpdateUserRequest request)
    {
        var user = await readRepository.GetByIdAsync(request.Id.ToString());

        if (user == null)
            throw new Exception($"User with id {request.Id} not found");

        user.Name = request.Name;
        user.Surname = request.Surname;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        writeRepository.Update(user);
        await writeRepository.SaveAsync();

        return MapToResponse(user);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await readRepository.GetByIdAsync(id.ToString());

        if (user == null)
            return false;

        writeRepository.Remove(user);
        await writeRepository.SaveAsync();

        return true;
    }

    private static UserResponse MapToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Status = user.Status
        };
    }
}
