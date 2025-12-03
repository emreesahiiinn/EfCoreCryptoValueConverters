using Application.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Repositories.Users;

public interface IUserReadRepository : IReadRepository<User>
{
}