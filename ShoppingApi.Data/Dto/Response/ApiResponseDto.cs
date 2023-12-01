namespace shopping.Dto.Response;

public class ApiResponseDto<T>
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}