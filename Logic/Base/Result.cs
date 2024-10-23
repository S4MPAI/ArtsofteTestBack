namespace Logic.Base;

public class Result<TValue>
{
    public readonly TValue Value;
    public readonly IEnumerable<Error> Errors;
    public readonly bool IsSuccess;

    public Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Errors = new List<Error>();
    }

    public Result(IEnumerable<Error> errors)
    {
        IsSuccess = false;
        Errors = errors;
        Value = default;
    }
}
