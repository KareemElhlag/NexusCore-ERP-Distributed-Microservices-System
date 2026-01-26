using MediatR;
namespace SharedKernel.NexusCore.Application.Abstractions
{
  /// <summary>
  /// Command: For operations that change data (Write)
  /// </summary>
  /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
