

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp.Portable;
using SoccerPlus.Infra.Http.Authentication.Request;
using SoccerPlus.Infra.Utils.Configurations;
using System.Net;
using System.Text;

namespace SoccerPlus.Infra.Http.Authentication
{
    public class AuthenticateService : BaseHttpService, IAuthenticateService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public AuthenticateService(
            HttpClient httpClient,
            IOptionsSnapshot<AppSettings> appSettings
            ) : base(httpClient)
        {
            _appSettings = appSettings.Value;
            _httpClient = httpClient;
        }

        public async Task<string> Authenticate(AuthenticationRequest requestUser)
        {
            var values = new Dictionary<string, string>
    {
        { "client_id", "client" },
        { "client_secret", "secret" },
        { "grant_type", "password" },
        { "username", requestUser?.Username },
        { "password", requestUser?.Password },
        { "scope", "default" }
    };

            var content = new FormUrlEncodedContent(values);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _appSettings.IS4)
            {
                Content = content
            };

            try
            {
                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    var error = $"Authentication error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while authenticating.", ex);
            }
        }


    }

}
