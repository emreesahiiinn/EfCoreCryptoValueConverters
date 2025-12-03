using System.Linq.Expressions;
using Domain.Entities.Common;

namespace Application.Repositories.BaseRepositories;

/// <summary>
///     Generic read-only repository interface for querying entities.
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Gets all entities of type T.
    /// </summary>
    /// <param name="tracking">Enable or disable change tracking. Default is true.</param>
    /// <returns>Queryable collection of all entities.</returns>
    IQueryable<T> GetAll(bool tracking = true);

    /// <summary>
    ///     Gets entities matching the specified predicate.
    /// </summary>
    /// <param name="method">Filter expression to apply.</param>
    /// <param name="tracking">Enable or disable change tracking. Default is true.</param>
    /// <returns>Queryable collection of filtered entities.</returns>
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);

    /// <summary>
    ///     Gets a single entity matching the specified predicate.
    /// </summary>
    /// <param name="method">Filter expression to apply.</param>
    /// <param name="tracking">Enable or disable change tracking. Default is true.</param>
    /// <returns>First matching entity or null if not found.</returns>
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

    /// <summary>
    ///     Gets an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier as a string.</param>
    /// <param name="tracking">Enable or disable change tracking. Default is true.</param>
    /// <returns>The entity with the specified ID or null if not found.</returns>
    Task<T?> GetByIdAsync(string id, bool tracking = true);
}