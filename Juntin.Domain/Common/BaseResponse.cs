namespace Domain.Common;

public class BaseResponse<T>
{
    public BaseResponse(T data)
    {
        Data = data;
    }

    public BaseResponse(Error error)
    {
        Data = default;
        Error = error;
    }

    public T? Data { get; set; }
    public Error Error { get; set; }
}