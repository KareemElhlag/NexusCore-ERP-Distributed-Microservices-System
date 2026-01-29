using MediatR;
using SharedKernel.NexusCore.Application.Abstractions;
using SharedKernel.NexusCore.Domain.Interfaces;
namespace SharedKernel.NexusCore.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;   
        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if ( request is not ICommand<TRequest>)
                return await next();
            
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await next();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return response;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
