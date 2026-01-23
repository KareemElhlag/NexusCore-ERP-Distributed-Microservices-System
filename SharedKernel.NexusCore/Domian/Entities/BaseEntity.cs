namespace SharedKernel.NexusCore.Domian.Entities
{
    /// <summary>
    /// Represents the base class for all entities in the application.
    /// </summary>
    /// <remarks>This class serves as a common base for entity types, providing a foundation for shared
    /// functionality  or metadata. Derived classes can extend this base to implement specific entity
    /// behavior.</remarks>
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime? CreatedAt { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; protected set; }
        public Guid? DeletedBy { get; private set; }
        // --- Domain Events Management ---

        private readonly List<object> _domainEvents = new();

        /// <summary>
        /// Gets the collection of domain events occurred within the entity lifecycle.
        /// These events are dispatched to the message bus (e.g., RabbitMQ) after a successful transaction.
        /// </summary>
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Adds a new domain event to the entity's internal event store.
        /// </summary>
        /// <param name="eventItem">The domain event object to be published.</param>
        public void AddDomainEvent(object eventItem) => _domainEvents.Add(eventItem);

        /// <summary>
        /// Clears all recorded domain events. Should be called after successful dispatch.
        /// </summary>
        public void ClearDomainEvents() => _domainEvents.Clear();

        // --- Audit Methods ---

        /// <summary>
        /// Sets initial auditing metadata for a newly created entity.
        /// </summary>
        /// <param name="userId">The unique identifier of the user creating the entity.</param>
        public void SetCreatedAudit(Guid userId)
        {
            if (CreatedAt is null)
            {
                CreatedAt = DateTime.UtcNow;
                CreatedBy = userId;
            }
        }

        /// <summary>
        /// Updates auditing metadata when an entity is modified.
        /// </summary>
        /// <param name="userId">The unique identifier of the user updating the entity.</param>
        public void SetUpdatedAudit(Guid userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        /// <summary>
        /// Marks the entity as deleted and records the deletion metadata.
        /// </summary>
        /// <param name="userId">The unique identifier of the user performing the soft delete.</param>
        public void SetDeletedAudit(Guid userId)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = userId;
        }

    }
}
