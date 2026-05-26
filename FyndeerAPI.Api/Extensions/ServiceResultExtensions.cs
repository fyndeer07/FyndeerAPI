using FyndeerAPI.Application.Common;
using FyndeerAPI.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FyndeerAPI.Api.Extensions;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult(this ServiceResult result, ControllerBase controller)
        => result.Error?.Type switch
        {
            ErrorType.NotFound   => controller.NotFound(ApiResponse.From(result)),
            ErrorType.Conflict   => controller.Conflict(ApiResponse.From(result)),
            ErrorType.Validation => controller.BadRequest(ApiResponse.From(result)),
            _                    => controller.Ok(ApiResponse.From(result))
        };

    public static IActionResult ToActionResult<T>(this ServiceResult<T> result, ControllerBase controller)
        => result.Error?.Type switch
        {
            ErrorType.NotFound   => controller.NotFound(ApiResponse<T>.From(result)),
            ErrorType.Conflict   => controller.Conflict(ApiResponse<T>.From(result)),
            ErrorType.Validation => controller.BadRequest(ApiResponse<T>.From(result)),
            _                    => controller.Ok(ApiResponse<T>.From(result))
        };

    public static IActionResult ToCreatedResult<T>(
        this ServiceResult<T> result,
        ControllerBase controller,
        string actionName,
        object routeValues)
    {
        if (!result.IsSuccess)
            return result.ToActionResult(controller);

        return controller.CreatedAtAction(actionName, routeValues, ApiResponse<T>.From(result));
    }
}
