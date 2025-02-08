namespace BrazilSurvival.BackEnd.Game.Exceptions;

// Excecao pra o client
public class InvalidPlayerStatsException : ProcessException
{
    private const string defaultMessage = "Invalid player stats object";
    private const string defaultDetail = "One or more fields in the player stats object that you send are invalid";

    public InvalidPlayerStatsException(string? message, string? detail) : base(message ?? defaultMessage, detail ?? defaultDetail, StatusCodes.Status400BadRequest)
    {
    }
}


// Excecao interna para servicos
public class NotFoundException : Exception
{
    public NotFoundException() : base("Not found")
    {
    }

    public NotFoundException(string? message) : base(message ?? "Resource not found")
    {
    }
}