using SoccerPlus.Application.Common;
using SoccerPlus.Application.Player.Models.Request;
using SoccerPlus.Domain.PlayerAggregate;
using SoccerPlus.Domain.SeedWork.Notification;
using SoccerPlus.Domain.SoccerMatchAggregate;
using SoccerPlus.Infra.Http.Mapbox;


namespace SoccerPlus.Application.Player.Services
{
    public class PlayerServices : BaseService, IPlayerServices
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ISoccerMatchRepository _soccerMatchRepository;
        private readonly IMapboxService _mapboxService;

        public PlayerServices(
            IPlayerRepository playerRepository,
            ISoccerMatchRepository soccerMatchRepository,
            IMapboxService mapboxService,
            INotification notification

            ) : base(notification)
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

            request.Longitude = log;
            request.Latitude = lat;


            var retorno = await _mapboxService.GetDistance(request.Longitude, request.Latitude, longtest, lattest);

            return retorno;

        }

        #endregion

    }
}
