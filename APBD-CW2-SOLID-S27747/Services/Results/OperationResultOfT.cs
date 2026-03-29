namespace APBD_CW2_SOLID_S27747.Services.Results;

public class OperationResult<T> : OperationResult
{
    public T? Data { get; }

    private OperationResult(bool success, string message, T? data)
        : base(success, message)
    {
        Data = data;
    }

    public static OperationResult<T> Ok(string message, T data)
    {
        return new OperationResult<T>(true, message, data);
    }

    public new static OperationResult<T> Fail(string message)
    {
        return new OperationResult<T>(false, message, default);
    }
}