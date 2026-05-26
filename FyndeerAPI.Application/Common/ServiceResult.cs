namespace FyndeerAPI.Application.Common;

public enum ErrorType
{
    NotFound,
    Conflict,
    Validation
}

public sealed record ServiceError(ErrorType Type, string Message);

public class ServiceResult
{
    public bool IsSuccess { get; }
    public string? Message { get; }
    public ServiceError? Error { get; }

    protected ServiceResult(bool isSuccess, string? message, ServiceError? error)
    {
        IsSuccess = isSuccess;
        Message = message;
        Error = error;
    }

    public static ServiceResult Success(string? message = null)
        => new(true, message, null);

    public static ServiceResult NotFound(string message)
        => new(false, null, new(ErrorType.NotFound, message));

    public static ServiceResult Conflict(string message)
        => new(false, null, new(ErrorType.Conflict, message));

    public static ServiceResult Validation(string message)
        => new(false, null, new(ErrorType.Validation, message));
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; }

    private ServiceResult(bool isSuccess, T? data, string? message, ServiceError? error)
        : base(isSuccess, message, error)
    {
        Data = data;
    }

    public static ServiceResult<T> Success(T data, string? message = null)
        => new(true, data, message, null);

    public new static ServiceResult<T> NotFound(string message)
        => new(false, default, null, new(ErrorType.NotFound, message));

    public new static ServiceResult<T> Conflict(string message)
        => new(false, default, null, new(ErrorType.Conflict, message));

    public new static ServiceResult<T> Validation(string message)
        => new(false, default, null, new(ErrorType.Validation, message));
}
