
namespace Core.Response.Abstractions
{
    public interface IBaseResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
