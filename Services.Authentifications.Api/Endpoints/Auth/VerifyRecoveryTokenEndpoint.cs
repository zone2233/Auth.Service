using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using FastEndpoints;
using FluentValidation;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class VerifyRecoveryTokenEndpoint : Endpoint<VerifyRecoveryTokenRequest>
    {
        private readonly IValidator<VerifyRecoveryTokenRequest> _validator;
        private readonly IResetTokenService _resetTokenService;
        private readonly IPasswordService _passwordService;
        public VerifyRecoveryTokenEndpoint(IValidator<VerifyRecoveryTokenRequest> validator, IResetTokenService resetTokenService, IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _resetTokenService = resetTokenService;
            _validator = validator;
        }
        public override void Configure()
        {
            Post("verify/token/");
            AllowAnonymous();
        }

        public override async Task HandleAsync(VerifyRecoveryTokenRequest verifyRecoveryTokenRequest, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(verifyRecoveryTokenRequest);
            if (result.IsValid)
            {
                if (await _resetTokenService.VerifyTokenReleaseTime(verifyRecoveryTokenRequest.RecoveryToken, cancellationToken))
                {
                    string username = await _resetTokenService.GetUsernameByToken(verifyRecoveryTokenRequest.RecoveryToken, cancellationToken);
                    await _passwordService.ModifyPasswordByUsername(username, verifyRecoveryTokenRequest.NewPassword, cancellationToken);
                    await Send.OkAsync();
                }
            }
            else
            {
                await Send.NotFoundAsync(cancellationToken);
            }
        }
    }
}
