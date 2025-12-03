using Application.Repositories.Users;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepositories;

namespace Persistence.Repositories.Users;

public class UserWriteRepository(EfCoreCryptoValueConvertersContext context)
    : WriteRepository<User>(context), IUserWriteRepository;