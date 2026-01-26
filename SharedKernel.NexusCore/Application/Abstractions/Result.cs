namespace SharedKernel.NexusCore.Application.Abstractions;

/// <summary>
/// A standardized wrapper for all API responses to ensure consistency.
/// </summary>
/// <typeparam name="T">The type of the data being returned.</typeparam>
public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }
    public List<string>? Errors { get; private set; }

    protected Result(bool isSuccess, T? data, string? message, List<string>? errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public static Result<T> Success(T data, string message = "Success")
        => new(true, data, message, null);

    public static Result<T> Failure(List<string> errors, string message = "Failure")
        => new(false, default, message, errors);
}