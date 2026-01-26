using SharedKernel.NexusCore.Domian.Entities;

namespace SharedKernel.NexusCore.Domain.Interfaces;

/// <summary>
/// Defines the standard CRUD operations for any entity of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the entity, must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity); // This should implement Soft Delete
}