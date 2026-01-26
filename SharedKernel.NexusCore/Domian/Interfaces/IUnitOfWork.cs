namespace SharedKernel.NexusCore.Domain.Interfaces;

/// <summary>
/// Ensures that multiple operations on the database are treated as a single atomic transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Persists all tracked changes to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts a new database transaction.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the current transaction to the database.
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Discards all changes made during the current transaction.
    /// </summary>
    Task RollbackTransactionAsync();
}