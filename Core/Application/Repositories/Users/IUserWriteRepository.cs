using Application.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Repositories.Users;

public interface IUserWriteRepository : IWriteRepository<User>
{
}