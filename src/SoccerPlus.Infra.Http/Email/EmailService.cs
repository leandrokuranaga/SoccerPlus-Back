using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using SoccerPlus.Infra.Http.Email.Request;
using SoccerPlus.Infra.Utils.Configurations;

namespace SoccerPlus.Infra.Http.Email;

public class EmailService : IEmailService
{
    private const string SUBJECT = "Alteração de senha do sistema";

    private readonly AppSettings _appSettings;

    public EmailService(
        IOptionsSnapshot<AppSettings> appSettings
        )
    {
        _appSettings = appSettings.Value;
    }

    public async Task<bool> SendEmail(ContentEmail content)
    {
        Configuration.Default.ApiKey["api-key"] = _appSettings.BrevoApiKey;

        var apiInstance = new TransactionalEmailsApi();
        SendSmtpEmailSender Email = new(_appSettings.SenderName, _appSettings.SenderEmail);
        SendSmtpEmailTo smtpEmailTo = new(content.Email, content.Name);
        List<SendSmtpEmailTo> To = new();
        To.Add(smtpEmailTo);

        string TextContent = $"Sua nova senha é {content.Password}";

        try
        {
            var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, TextContent, SUBJECT);
            return await System.Threading.Tasks.Task.Run(() =>
            {
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                var hasResponse = result.MessageId == string.Empty ? false : true;
                return hasResponse;
            });
        }
        catch (Exception e)
        {
            throw;
        }

    }
}

