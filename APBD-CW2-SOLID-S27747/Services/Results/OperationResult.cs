namespace APBD_CW2_SOLID_S27747.Services.Results;

public class OperationResult
{
    public bool Success { get; }
    public string Message { get; }

    protected OperationResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static OperationResult Ok(string message)
    {
        return new OperationResult(true, message);
    }

    public static OperationResult Fail(string message)
    {
        return new OperationResult(false, message);
    }
}