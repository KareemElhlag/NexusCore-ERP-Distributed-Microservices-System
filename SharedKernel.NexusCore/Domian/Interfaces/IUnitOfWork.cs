namespace SharedKernel.NexusCore.Domain.Interfaces;

/// <summary>
/// Ensures that multiple operations on the database are treated as a single atomic transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Starts a new database transaction.
    /// </summary>
    Task BeginTransactionAsync( CancellationToken ct= default);

    /// <summary>
    /// Commits the current transaction to the database.
    /// </summary>
    Task CommitTransactionAsync(CancellationToken ct = default);

    /// <summary>
    /// Discards all changes made during the current transaction.
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken ct = default);
}