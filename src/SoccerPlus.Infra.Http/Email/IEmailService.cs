using SoccerPlus.Infra.Http.Email.Request;

namespace SoccerPlus.Infra.Http.Email
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(ContentEmail content);
    }
}
