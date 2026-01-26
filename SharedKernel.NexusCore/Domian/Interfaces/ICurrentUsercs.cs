namespace SharedKernel.NexusCore.Domian.Interfaces;

/// <summary>
/// Provides access to the current authenticated user's information.
/// This is used for auditing and security checks across all services.
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the unique identifier of the current user.
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    /// Gets the username of the current user.
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// Checks if the current request is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}