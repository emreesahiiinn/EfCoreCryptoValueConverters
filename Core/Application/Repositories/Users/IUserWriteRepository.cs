using Application.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Repositories.Users;

/// <summary>
///     Write repository interface for User entities.
/// </summary>
public interface IUserWriteRepository : IWriteRepository<User>
{
}