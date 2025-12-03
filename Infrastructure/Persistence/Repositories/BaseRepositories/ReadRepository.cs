using System.Linq.Expressions;
using Application.Repositories.BaseRepositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.BaseRepositories;

/// <summary>
///     Generic read-only repository implementation for querying entities.
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity.</typeparam>
public class ReadRepository<T>(DbContext context) : IReadRepository<T>
    where T : BaseEntity
{
    /// <inheritdoc />
    public DbSet<T> Table => context.Set<T>();

    /// <inheritdoc />
    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    /// <inheritdoc />
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    /// <inheritdoc />
    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id.ToString() == id);
    }
}