using Microsoft.AspNetCore.Mvc;
using SoccerPlus.Application.User.Model.Request;
using SoccerPlus.Application.User.Services;
using SoccerPlus.Domain.SeedWork.Notification;

namespace SoccerPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService,
            INotification notification
            ) : base(notification)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest loginModel)
            => Response(await _userService.AuthenticateAsync(loginModel).ConfigureAwait(false));

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequest request)
            => Response(await _userService.RegisterAsync(request).ConfigureAwait(false));
    }
}
