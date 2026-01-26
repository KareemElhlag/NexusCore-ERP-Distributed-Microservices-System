using MediatR;
namespace SharedKernel.NexusCore.Domian.Event
{
    /// <summary>
    /// Represents a domain event that occurs within the application's domain.
    /// </summary>
    public interface IDomainEventcs : INotification
    {
        /// <summary>
        /// DATE AND TIME WHEN THE EVENT OCCURRED
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
