using Application.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Repositories.Users;

/// <summary>
///     Read-only repository interface for User entities.
/// </summary>
public interface IUserReadRepository : IReadRepository<User>
{
}