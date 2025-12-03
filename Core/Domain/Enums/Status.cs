namespace Domain.Enums;

/// <summary>
///     Represents the lifecycle status of an entity.
/// </summary>
public enum Status
{
    /// <summary>
    ///     Entity has been soft-deleted but remains in the database.
    /// </summary>
    Deleted,

    /// <summary>
    ///     Entity exists but is temporarily disabled or inactive.
    /// </summary>
    Inactive,

    /// <summary>
    ///     Entity is active and operational.
    /// </summary>
    Active
}