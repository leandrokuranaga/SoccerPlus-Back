using SoccerPlus.Application.Player.Models.Request;
using SoccerPlus.Domain.PlayerAggregate;
using SoccerPlus.Domain.SoccerMatchAggregate;
using SoccerPlus.Infra.Http.Mapbox;


namespace SoccerPlus.Application.Player.Services
{
    public class PlayerServices : IPlayerServices
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ISoccerMatchRepository _soccerMatchRepository;
        private readonly IMapboxService _mapboxService;

        public PlayerServices(
            IPlayerRepository playerRepository,
            ISoccerMatchRepository soccerMatchRepository,
            IMapboxService mapboxService
            )
        {
            _playerRepository = playerRepository;
            _soccerMatchRepository = soccerMatchRepository;
            _mapboxService = mapboxService;
        }

        #region FindMatch
        public async Task<double> FindMatchAsync(PlayerRequest request)
        {
            var log = "-23.116873";
            var lat = "-47.224025";


            var user = await _playerRepository.GetByIdAsync(request.Id, false);            

            var field = await _soccerMatchRepository.GetByIdAsync(request.IdField, false);
            var longtest = "-23.094522";
                var lattest = "-47.210527";


            var retorno = await _mapboxService.GetDistance(log, lat, longtest, lattest);

            return retorno;

        }

        #endregion

    }
}
