using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.BaseRepositories;

/// <summary>
///     Base repository interface providing access to the underlying DbSet.
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Gets the DbSet for the entity type.
    /// </summary>
    DbSet<T> Table { get; }
}