using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerPlus.Application.SoccerMatch.Models.Request;
using SoccerPlus.Application.SoccerMatch.Models.Response;

namespace SoccerPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SoccerMatchController : ControllerBase
    {

        [HttpGet("findMatch")]
        public async Task<ActionResult<SoccerMatchResponse>> FindMatch([FromQuery] SoccerMatchRequest soccerMatch)
        {
            return Ok(soccerMatch);
        }

        [HttpPost("findPlayers")]
        public async Task<ActionResult<SoccerMatchResponse>> FindPlayers([FromBody] SoccerMatchRequest soccerMatch)
        {
            return Ok(soccerMatch);
        }

        [HttpPost("scheduleMatch")]
        public async Task<ActionResult<SoccerMatchResponse>> CreateMatch([FromBody] SoccerMatchRequest soccerMatch)
        {
            return Ok(soccerMatch);
        }

        [HttpPost("createTeam")]
        public async Task<ActionResult<SoccerMatchResponse>> CreateTeam([FromBody] SoccerMatchRequest soccerMatch)
        {
            return Ok(soccerMatch);
        }

    }
}
