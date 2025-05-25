using BLL.Services.Abstractions;
using Core.Entities;
using Core.Models;
using Core.Response;
using Core.Static;
using System.Security.Claims;

namespace BLL.Services
{
    public class AccountService: IAccountService
    {
        private IService<User> _userService { get; set; }

        public AccountService(IService<User> userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            var users = await _userService.GetAllAsync();
            var user = users.Data.FirstOrDefault(u => u.Email == model.Email);

            if (user is not null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Message = "There is user with the same login",
                    //StatusCode = StatusCode.Error
                };
            }

            Profile profile = new Profile();

            user = new User()
            {
                Email = model.Email,
                PasswordHash = HashPasswordHelper.HashPassword(model.Password),
                Profile = profile
            };

            await _userService.AddAsync(user);

            var result = AuthHelper.Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Message = "Registered",
                //StatusCode = StatusCode.Ok
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            var users = await _userService.GetAllAsync();
            var user = users.Data.FirstOrDefault(u => u.Email == model.Email);

            if (user is null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Message = "There is no user with this login",
                    //StatusCode = StatusCode.Error
                };
            }

            if (user.PasswordHash != HashPasswordHelper.HashPassword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Message = "Password is not correct",
                    //StatusCode = StatusCode.Error
                };
            }

            var result = AuthHelper.Authenticate(user);

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Message = "Registered",
                //StatusCode = StatusCode.Ok
            };


        }
    }
}
