using Application.Config;
using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IOptions<Settings> _settings;


        public SendEmailService(IOptions<Settings> settings)
        {
            _settings = settings;

        }
        public async Task<Response> SendRecoveryEmail(EmailRequest emailRequest)
        {
            var apiKeyFromSettings = _settings.Value.SendGridApiKey;
            //var apiKey = Environment.GetEnvironmentVariable(apiKeyFromSettings);
            var client = new SendGridClient(apiKeyFromSettings);
            var to = new EmailAddress(emailRequest.EmailAdress);
            var from = new EmailAddress("test@example.com");
            var subject = "Recover your password!";
            var body = "Hello here is the link for recovering your password: ";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
