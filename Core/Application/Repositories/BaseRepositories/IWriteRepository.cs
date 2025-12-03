using Domain.Entities.Common;

namespace Application.Repositories.BaseRepositories;

/// <summary>
///     Generic write repository interface for modifying entities.
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Adds a new entity to the context.
    /// </summary>
    /// <param name="model">The entity to add.</param>
    /// <returns>True if the entity was successfully marked as added.</returns>
    Task<bool> AddAsync(T model);

    /// <summary>
    ///     Adds multiple entities to the context.
    /// </summary>
    /// <param name="models">Collection of entities to add.</param>
    /// <returns>True if entities were successfully marked as added.</returns>
    Task<bool> AddRangeAsync(List<T> models);

    /// <summary>
    ///     Updates an existing entity in the context.
    /// </summary>
    /// <param name="model">The entity to update.</param>
    /// <returns>True if the entity was successfully marked as modified.</returns>
    bool Update(T model);

    /// <summary>
    ///     Removes multiple entities from the context.
    /// </summary>
    /// <param name="models">Collection of entities to remove.</param>
    /// <returns>True if entities were successfully marked for deletion.</returns>
    bool RemoveRange(List<T> models);

    /// <summary>
    ///     Removes an entity from the context.
    /// </summary>
    /// <param name="model">The entity to remove.</param>
    /// <returns>True if the entity was successfully marked as deleted.</returns>
    bool Remove(T model);

    /// <summary>
    ///     Removes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier as a string.</param>
    /// <returns>True if the entity was found and marked for deletion.</returns>
    Task<bool> RemoveAsync(string id);

    /// <summary>
    ///     Persists all pending changes to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveAsync();
}