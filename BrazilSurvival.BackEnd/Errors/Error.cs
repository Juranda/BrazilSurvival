namespace BrazilSurvival.BackEnd.Errors;

public class Error
{
    public int Code { get; set; }
    public string Message { get; set; }
    public ErrorType Type { get; set; }
    public string[] Errors { get; set; }

    public Error(ErrorType error, string message = "")
    {
        Errors = [];
        Type = error;
        Code = (int)error;
        Message = message;
    }
    public Error(ErrorType error, string message = "", params string[] errors)
    {
        Errors = errors;
        Type = error;
        Code = (int)error;
        Message = message;
    }

    public static Error NotFound(string message = "Resource not found") => new(ErrorType.NOT_FOUND, message);
    public static Error InvalidArgument(string message = "Invalid argument") => new(ErrorType.INVALID_ARGUMENT, message);
    public static Error Unauthorized(string message = "Unauthorized to this action or resource") => new(ErrorType.UNAUTHORIZED, message);

    public enum ErrorType
    {
        NOT_FOUND = 1,
        INVALID_ARGUMENT = 2,
        UNAUTHORIZED = 3
    }
}
