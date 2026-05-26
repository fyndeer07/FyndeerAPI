using FyndeerAPI.Application.Common;

namespace FyndeerAPI.Api.Responses;

public class ApiResponse
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public ApiError? Error { get; init; }

    public static ApiResponse From(ServiceResult result) => new()
    {
        IsSuccess = result.IsSuccess,
        Message = result.Message,
        Error = result.Error is null ? null : ApiError.From(result.Error)
    };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; init; }

    public static ApiResponse<T> From(ServiceResult<T> result) => new()
    {
        IsSuccess = result.IsSuccess,
        Message = result.Message,
        Data = result.Data,
        Error = result.Error is null ? null : ApiError.From(result.Error)
    };
}

public sealed record ApiError(string Type, string Message)
{
    public static ApiError From(ServiceError error) => new(error.Type.ToString(), error.Message);
}
