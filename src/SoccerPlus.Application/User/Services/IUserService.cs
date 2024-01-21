using Microsoft.AspNetCore.Identity;
using SoccerPlus.Application.User.Model.Request;
using SoccerPlus.Application.User.Model.Response;

namespace SoccerPlus.Application.User.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(UserRequest requestUser);
        Task<UserCreatedResponse> RegisterAsync(UserRequest requestUser);

    }

}
