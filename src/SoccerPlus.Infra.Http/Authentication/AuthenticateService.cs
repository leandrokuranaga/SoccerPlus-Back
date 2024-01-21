using Microsoft.Extensions.Options;
using SoccerPlus.Infra.Http.Authentication.Request;
using SoccerPlus.Infra.Utils.Configurations;

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
            string responseContent = string.Empty;

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
                    responseContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while authenticating.", ex);
            }
            return responseContent;
        }
    }
}
