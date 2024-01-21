using SoccerPlus.Application.Player.Models.Request;

namespace SoccerPlus.Application.Player.Services
{
    public interface IPlayerServices
    {
        public Task<double> FindMatchAsync(PlayerRequest request);
    }
}
