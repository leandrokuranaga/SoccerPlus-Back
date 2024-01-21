using Microsoft.AspNetCore.Mvc;
using SoccerPlus.Application.User.Model.Request;
using SoccerPlus.Application.User.Model.Response;
using SoccerPlus.Application.User.Services;

namespace SoccerPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest loginModel)
        {
            try
            {
                var token = await _userService.AuthenticateAsync(loginModel);
                if (token == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                return Ok(token);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserCreatedResponse>> Register(UserRequest request)
        {
            try
            {
                var user = await _userService.RegisterAsync(request);

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Tratamento de erros
                return BadRequest(ex.Message);
            }
        }
    }
}
