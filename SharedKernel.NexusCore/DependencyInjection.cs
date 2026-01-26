using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace SharedKernel.NexusCore;

/// <summary>
/// Provides extension methods for registering shared kernel services, including MediatR and FluentValidation,  in the
/// dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers MediatR and FluentValidation for the calling assembly.
    /// </summary>
    public static IServiceCollection AddSharedKernel(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}