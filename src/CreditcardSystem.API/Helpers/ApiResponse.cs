namespace CreditcardSystem.API.Helpers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string message { get; set; }
    public T? Data { get; set; }
}
