
namespace SoccerPlus.Application.User.Model.Response
{
    public class UserTokenResponse
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }

}
