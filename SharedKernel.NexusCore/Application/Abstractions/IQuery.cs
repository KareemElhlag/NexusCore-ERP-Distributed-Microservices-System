namespace SharedKernel.NexusCore.Application.Abstractions
{
    /// <summary>
    /// For operations that retrieve data (Read)
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery <TResponse> : MediatR.IRequest<Result<TResponse>>
    {
    }
}
