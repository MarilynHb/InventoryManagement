namespace InventoryManagement.Shared;
public class ServiceResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public static ServiceResult<T> Sucess(T data) => new()
    {
        Success = true,
        Data = data
    };

    public static ServiceResult<T> Failure(string message) => new()
    {
        Success = false,
        ErrorMessage = message,
    };
}