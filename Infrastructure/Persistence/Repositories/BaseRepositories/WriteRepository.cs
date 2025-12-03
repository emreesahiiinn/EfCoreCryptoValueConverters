using Application.Repositories.BaseRepositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Repositories.BaseRepositories;

/// <summary>
///     Generic write repository implementation for modifying entities.
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public class WriteRepository<T>(DbContext context) : IWriteRepository<T> where T : BaseEntity
{
    /// <inheritdoc />
    public DbSet<T> Table => context.Set<T>();

    /// <inheritdoc />
    public async Task<bool> AddAsync(T model)
    {
        var entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    /// <inheritdoc />
    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return true;
    }

    /// <inheritdoc />
    public bool Remove(T model)
    {
        var entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    /// <inheritdoc />
    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> RemoveAsync(string id)
    {
        var model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        return Remove(model!);
    }

    /// <inheritdoc />
    public bool Update(T model)
    {
        EntityEntry entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    /// <inheritdoc />
    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}