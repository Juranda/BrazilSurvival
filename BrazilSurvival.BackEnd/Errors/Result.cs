namespace BrazilSurvival.BackEnd.Errors;

#pragma warning disable CS8625
#pragma warning disable CS8618
#pragma warning disable CS8601
public class Result<T>
{
    public T Value { get; }
    public Error Error { get; }
    public bool HasValue { get; }
    public bool HasError { get; }

    public Result(T value)
    {
        Value = value;
        HasValue = true;

        Error = default;
        HasError = false;
    }

    public Result(Error error)
    {
        Value = default;
        HasValue = false;

        Error = error;
        HasError = true;
    }


    public static Result<T> WithError(Error error) => new(error);
    public static Result<T> WithValue(T value) => new(value);

    public static implicit operator Result<T>(T value) => Result<T>.WithValue(value);
    public static implicit operator Result<T>(Error error) => Result<T>.WithError(error);
}

#pragma warning restore CS8601
#pragma warning restore CS8618
#pragma warning restore CS8625