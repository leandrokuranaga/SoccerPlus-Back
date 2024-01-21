using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerPlus.Application.Player.Models.Request;
using SoccerPlus.Application.Player.Models.Response;
using SoccerPlus.Application.Player.Services;

namespace SoccerPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerServices _playerServices;

        public PlayerController(IPlayerServices playerServices)
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
        public async Task<ActionResult<double>> FindMatchAsync(PlayerRequest request)
        {
            var distance = await _playerServices.FindMatchAsync(request);
            return distance;
        }
    }
}
