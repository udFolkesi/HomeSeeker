using Core.Response.Abstractions;

namespace BLL.Services.Abstractions
{
    public interface IService<T>
    {
        Task<IBaseResponse<T>> GetByIdAsync(int id);
        Task<IBaseResponse<ICollection<T>>> GetAllAsync();
        Task<IBaseResponse<bool>> AddAsync(T entity);
        Task<IBaseResponse<bool>> UpdateAsync(T entity);
        Task<IBaseResponse<bool>> DeleteAsync(int id);
    }
}
