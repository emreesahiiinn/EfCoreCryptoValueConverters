using Application.Repositories.Users;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepositories;

namespace Persistence.Repositories.Users;

/// <summary>
///     Write repository implementation for User entities.
/// </summary>
public class UserWriteRepository(EfCoreCryptoValueConvertersContext context)
    : WriteRepository<User>(context), IUserWriteRepository;