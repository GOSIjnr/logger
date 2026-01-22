namespace Logger.Application.Models;

public partial record OperationResult<T>
{
    public static OperationResult<T> Success(T data)
        => new("SUCCESS", "Operation completed successfully.", null, data);
}
