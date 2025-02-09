using Microsoft.AspNetCore.Diagnostics;

namespace BrazilSurvival.BackEnd.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> logger;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        this.logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                Exception = exception,
                HttpContext = httpContext,
                ProblemDetails = new()
                {
                    Title = "Something went wrong",
                    Status = StatusCodes.Status500InternalServerError,
                    Extensions = new Dictionary<string, object?>
                    {
                        { "whosToBlame", "We" }
                    }
                },
            }
        );
    }
}
