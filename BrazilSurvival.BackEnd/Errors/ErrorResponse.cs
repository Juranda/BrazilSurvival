using Microsoft.AspNetCore.Mvc;

namespace BrazilSurvival.BackEnd.Errors;

public class ErrorResponse
{
    public string Message { get; }
    public string Detail { get; }
    public int StatusCode { get; }
    public string[] Errors { get; }

    public ErrorResponse(string message, string detail = "", int statusCode = StatusCodes.Status500InternalServerError)
    {
        StatusCode = statusCode;
        Detail = detail;
        Message = message;
        Errors = [];
    }

    public ErrorResponse(string message, string[] errors, string detail = "", int statusCode = StatusCodes.Status500InternalServerError)
    {
        StatusCode = statusCode;
        Detail = detail;
        Message = message;
        Errors = errors;
    }

    public static IActionResult InternalServerError(string message = "Something went wrong") => new ErrorResponse(message, statusCode: StatusCodes.Status500InternalServerError).ToActionResult();
    public static IActionResult NotFound(string message = "Item not found") => new ErrorResponse(message, statusCode: StatusCodes.Status404NotFound).ToActionResult();
    public static IActionResult NotFound(Error error) => NotFound(error.Message);
    public static IActionResult InvalidArgument(string message = "Invalid argument", params string[] errors) => new ErrorResponse(message, errors, statusCode: StatusCodes.Status400BadRequest).ToActionResult();
    public IActionResult ToActionResult()
    {
        Dictionary<string, object?> extensions = new()
        {
            { "whosToBlame",  DefineWhosToBlame(StatusCode) },
        };

        if (Errors.Length > 0)
        {
            extensions.Add("errors", Errors);
        }

        var result = new ObjectResult(new ProblemDetails
        {
            Title = Message,
            Detail = Detail,
            Status = StatusCode,
            Extensions = extensions,
        });

        return result;
    }

    private static string DefineWhosToBlame(int statusCode)
    {
        if (statusCode >= 300 && statusCode < 400)
        {
            return "For now, nobody";
        }
        else if (statusCode >= 400 && statusCode < 500)
        {
            return "You";
        }

        return "We";
    }
}