using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Game.Exceptions;

public class ProcessErrorExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<ProcessErrorExceptionHandler> logger;

    public ProcessErrorExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<ProcessErrorExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        int statusCode;
        string whosToBlame = "";

        if (exception is ProcessException processException)
        {
            statusCode = processException.statusCode;
        }
        else
        {
            statusCode = 500;
        }


        if (statusCode >= 300 && statusCode < 400)
        {
            whosToBlame = "For now, nobody";
        }
        else if (statusCode >= 400 && statusCode < 500)
        {
            whosToBlame = "You";
        }
        if (statusCode >= 500 && statusCode < 600)
        {
            whosToBlame = "We";
        }

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = statusCode,
            Extensions = {
                {
                    "whosToBlame", whosToBlame
                }
            }
        };

        httpContext.Response.StatusCode = statusCode;

        logger.LogInformation("{detail}", new { detail = problemDetails.ToString() });

        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                Exception = exception,
                HttpContext = httpContext,
                ProblemDetails = problemDetails
            }
        );
    }
}


public class ProcessException : Exception
{
    public readonly string? detail;
    public readonly int statusCode;

    public ProcessException(string message, string? detail, int statusCode) : base(message)
    {
        this.detail = detail;
        this.statusCode = statusCode;
    }


    public static ProcessException InternalServerError() => new ProcessException("Internal server error", "", 500);
    public static ProcessException NotFound() => new ProcessException("Item not found", "", 500);
}