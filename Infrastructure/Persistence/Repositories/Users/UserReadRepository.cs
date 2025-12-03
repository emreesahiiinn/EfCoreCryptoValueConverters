using Application.Repositories.Users;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepositories;

namespace Persistence.Repositories.Users;

/// <summary>
///     Read-only repository implementation for User entities.
/// </summary>
public class UserReadRepository(EfCoreCryptoValueConvertersContext context)
    : ReadRepository<User>(context), IUserReadRepository;