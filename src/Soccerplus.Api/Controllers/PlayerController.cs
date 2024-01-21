using Microsoft.AspNetCore.Mvc;
using SoccerPlus.Application.Player.Models.Request;
using SoccerPlus.Application.Player.Models.Response;
using SoccerPlus.Application.Player.Services;
using SoccerPlus.Domain.SeedWork.Notification;

namespace SoccerPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : BaseController
    {
        private readonly IPlayerServices _playerServices;

        public PlayerController(
            IPlayerServices playerServices,
            INotification notification
            ) : base(notification)
        {
            _playerServices = playerServices;
        }

        [HttpGet("{playerId}")]
        public async Task<ActionResult<PlayerResponse>> GetPlayer([FromRoute] int playerId)
        {
            return Ok(new PlayerResponse());
        }

        [HttpPost("createPlayer")]
        public async Task<ActionResult<PlayerResponse>> CreatePlayer([FromBody] PlayerResponse player)
        {
            return Ok(player);
        }

        [HttpPatch("updatePlayer")]
        public async Task<ActionResult<PlayerResponse>> UpdatePlayer([FromBody] PlayerResponse player)
        {
            return Ok(player);
        }

        [HttpPost("ratePlayer")]
        public async Task<ActionResult<PlayerResponse>> RatePlayer([FromBody] PlayerResponse player)
        {
            return Ok(player);
        }

        [HttpPost("find-match")]
        public async Task<IActionResult> FindMatchAsync(PlayerRequest request)
            => Response(await _playerServices.FindMatchAsync(request).ConfigureAwait(false));
    }
}
