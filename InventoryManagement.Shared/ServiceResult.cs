namespace InventoryManagement.Shared;
public class ServiceResult<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
    public bool IsNotFound { get; init; }
    public static ServiceResult<T> Sucess(T data) => new()
    {
        Success = true,
        Data = data
    };

    public static ServiceResult<T> Failure(string message, bool isNotFound = false) => new()
    {
        Success = false,
        ErrorMessage = message,
        IsNotFound = isNotFound
    };
}