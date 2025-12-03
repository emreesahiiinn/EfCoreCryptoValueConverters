using Domain.Enums;

namespace Domain.Entities.Common;

/// <summary>
///     Base entity class providing common properties for all domain entities.
/// </summary>
/// <remarks>
///     All entities in the domain should inherit from this class to ensure
///     consistent tracking of creation, modification times and entity status.
/// </remarks>
public class BaseEntity
{
    /// <summary>
    ///     Unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     UTC timestamp when the entity was created.
    ///     Automatically set by SaveChangesAsync in DbContext.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     UTC timestamp when the entity was last updated.
    ///     Automatically set by SaveChangesAsync in DbContext.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Current status of the entity (Active, Inactive, Deleted).
    /// </summary>
    public Status Status { get; set; }
}