using Core.Models;
using Core.Response;
using System.Security.Claims;

namespace BLL.Services.Abstractions
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
