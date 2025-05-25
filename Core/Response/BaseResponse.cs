using Core.Response.Abstractions;

namespace Core.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public static BaseResponse<T> Ok(T data) =>
            new() { Data = data, Success = true };
        public static BaseResponse<T> Fail(string message) =>
            new() { Success = false, Message = message };

    }
}
