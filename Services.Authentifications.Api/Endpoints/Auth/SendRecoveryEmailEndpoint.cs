using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using FastEndpoints;
using FluentValidation;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class SendRecoveryEmailEndpoint : Endpoint<EmailRequest>
    {
        private readonly IValidator<EmailRequest> _validator;
        private readonly IResetTokenService _resetTokenService;
        private readonly ISendEmailService _sendEmailService;
        public SendRecoveryEmailEndpoint(IValidator<EmailRequest> validator, IResetTokenService resetTokenService, ISendEmailService sendEmailService)
        {
            _resetTokenService = resetTokenService;
            _validator = validator;
            _sendEmailService = sendEmailService;
        }
        public override void Configure()
        {
            Post("recovery/email/");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(emailRequest);
            if(result.IsValid)
            {
                await _resetTokenService.Insert(emailRequest.EmailAdress, cancellationToken);
                await _sendEmailService.SendRecoveryEmail(emailRequest);
                await Send.OkAsync();
            }else
            {
                await Send.NotFoundAsync(cancellationToken);
            }
        }
    }
}
