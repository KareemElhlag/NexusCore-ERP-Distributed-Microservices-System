using MediatR;
using FluentValidation;
using SharedKernel.NexusCore.Application.Abstractions;

namespace SharedKernel.NexusCore.Application.Behaviors
{
    /// <summary>
    /// Validates every request (Command / Query) using FluentValidation /// before it reaches the handler.
    /// </summary>
    /// <typeparam name="TRequst"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
        where TRequest : IRequest<Result<TResponse>>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        ///   Handeler method to validate the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="next"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationReesults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationReesults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Count != 0)
            {
                var errors = failures.Select(f => f.ErrorMessage).ToList();
                return Result<TResponse>.Failure(errors, "Validation Failed");
            }
            return await next();
        }
    }
}
