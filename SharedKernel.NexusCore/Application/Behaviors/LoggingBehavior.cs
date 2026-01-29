
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.NexusCore.Application.Abstractions;

namespace SharedKernel.NexusCore.Application.Behaviors
{
    /// <summary>
    ///     Logger  
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        /// <summary>
        /// handel Logger 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("Handling {RequestName} with data: {@Request}", requestName, request);
            var response = next();
            if (response is Result<object> result && !result.IsSuccess)
            {
                _logger.LogWarning("⚠️ {RequestName} failed with errors: {@Errors}", requestName, result.Errors);
            }
            _logger.LogInformation("Handled {RequestName} with response: {@Response}", requestName, response);
            return response;
        }
    }
}
