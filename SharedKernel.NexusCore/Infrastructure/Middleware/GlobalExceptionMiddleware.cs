using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using SharedKernel.NexusCore.Application.Abstractions;

namespace SharedKernel.NexusCore.Infrastructure.Middleware
{
    /// <summary>
    /// Handles global exceptions and returns standardized error responses.
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        /// <summary>
        /// constructor for GlobalExceptionMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// records unhandled exceptions and returns a standardized error response.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Massege}" , ex.Message);
                await HandleExceptionAsync(context, ex);

            }


        }


        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
                       context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = Result<string>.Failure(new List<string> { "An unexpected error occurred. Please try again later." }, "Internal Server Error");
            var jsonResponse = JsonSerializer.Serialize(result);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
