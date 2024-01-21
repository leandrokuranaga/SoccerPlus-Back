namespace SoccerPlus.Infra.Http.Mapbox;

    public interface IMapboxService
    {
        Task<double> GetDistance(string lati, string longi, string lat, string lon);
    }


