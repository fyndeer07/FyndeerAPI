using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FyndeerAPI.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var stopwatch = Stopwatch.StartNew();

        _logger.LogDebug("Handling {RequestName} {@Request}", requestName, request);

        try
        {
            var response = await next();

            stopwatch.Stop();

            if (response is ServiceResult { IsSuccess: false } failed)
            {
                _logger.LogWarning(
                    "{RequestName} failed in {ElapsedMs}ms — [{ErrorType}] {ErrorMessage}",
                    requestName,
                    stopwatch.ElapsedMilliseconds,
                    failed.Error?.Type,
                    failed.Error?.Message);
            }
            else
            {
                _logger.LogInformation(
                    "{RequestName} succeeded in {ElapsedMs}ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogError(
                ex,
                "{RequestName} threw an unhandled exception after {ElapsedMs}ms",
                requestName,
                stopwatch.ElapsedMilliseconds);

            throw;
        }
    }
}
