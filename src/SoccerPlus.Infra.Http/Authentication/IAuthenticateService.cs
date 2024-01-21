using SoccerPlus.Infra.Http.Authentication.Request;

namespace SoccerPlus.Infra.Http.Authentication
{
    public interface IAuthenticateService
    {
        Task<string> Authenticate(AuthenticationRequest request);
    }

}
