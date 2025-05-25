using BLL.Services.Abstractions;
using Core.Entities;
using Core.Models;
using Core.Response;
using Core.Response.Abstractions;
using DAL.Repositories.Abstractions;

namespace BLL.Services
{
    public class UserService: IService<User>
    {
        private IBaseRepository<User> _repository { get; set; }
        //private readonly IWebHostEnvironment _hostingEnvironment;

        public UserService(IBaseRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IBaseResponse<bool>> AddAsync(User user)
        {
            IBaseResponse<bool> response = new BaseResponse<bool>();

            if (await _repository.Create(user))
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "Пользователь добавлен";
                response.Data = true;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "Пользователь не добавлен";
                response.Data = false;
            }

            return response;
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(int id)
        {
            IBaseResponse<bool> response = new BaseResponse<bool>();

            if (await _repository.Delete(id))
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "Пользователь удален";
                response.Data = true;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "Пользователь не удален";
                response.Data = false;
            }

            return response;
        }

        public async Task<IBaseResponse<User>> GetByIdAsync(int id)
        {
            IBaseResponse<User> response = new BaseResponse<User>();

            User user = await _repository.GetById(id);

            if (user is not null)
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "Пользователь найден";
                response.Data = user;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "Пользователь не найден";
                response.Data = null;
            }

            return response;
        }

        public async Task<IBaseResponse<ICollection<User>>> GetAllAsync()
        {
            IBaseResponse<ICollection<User>> response = new BaseResponse<ICollection<User>>();

            ICollection<User> users = await _repository.GetAll();

            if (users.Count != 0)
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "Пользователи найдены";
                response.Data = users;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "Пользователи не найдены";
                response.Data = users;
            }

            return response;
        }

        public async Task<IBaseResponse<bool>> UpdateAsync(User user)
        {
            IBaseResponse<bool> response = new BaseResponse<bool>();

            //ProccesTheContent(user.Profile);

            if (await _repository.Update(user))
            {
                //response.StatusCode = StatusCode.Ok;
                response.Message = "User is changed";
                response.Data = true;
            }
            else
            {
                //response.StatusCode = StatusCode.Error;
                response.Message = "User is not changed";
                response.Data = false;
            }

            return response;
        }
    }
}
