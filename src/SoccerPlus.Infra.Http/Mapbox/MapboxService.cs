
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SoccerPlus.Infra.Utils.Configurations;

namespace SoccerPlus.Infra.Http.Mapbox;

public class MapboxService : BaseHttpService, IMapboxService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public MapboxService(
        HttpClient httpClient,
        IOptionsSnapshot<AppSettings> appSettings
        ) : base(httpClient)
    {
        _appSettings = appSettings.Value;
        _httpClient = httpClient;
    }

    public async Task<double> GetDistance(string lati, string longi, string lat, string lon)
    {
        var response = await _httpClient.GetAsync($"https://api.mapbox.com/directions/v5/mapbox/driving/{longi}%2C{lati}%3B{lon}%2C{lat}?alternatives=true&geometries=geojson&language=en&overview=full&steps=true&access_token={_appSettings.MapboxApiKey}").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(); 
            var data = JObject.Parse(content); 

            var distance = data["routes"][0]["distance"].Value<double>() / 1000;
            var roundedDistance = Math.Round(distance, 1); // Arredonda para uma casa decimal


            return roundedDistance;
        }
        else
        {

            Console.WriteLine($"Erro ao obter a distância: {response.StatusCode}");
            return -1; 
        }
    }
}

