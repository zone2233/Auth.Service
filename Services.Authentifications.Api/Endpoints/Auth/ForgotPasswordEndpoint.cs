using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using FastEndpoints;
using FluentValidation;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class ForgotPasswordEndpoint : FastEndpoints.Endpoint<ForgotPasswordRequest>
    {
        private readonly IValidator<ForgotPasswordRequest> _validator;
        private readonly ITOTPService _totpService;
        private readonly IUsersService _usersService;
        private readonly IPasswordService _passwordService;

        public ForgotPasswordEndpoint(IValidator<ForgotPasswordRequest> validator, ITOTPService totpService, IUsersService usersService, IPasswordService passwordService)
        {

            _totpService = totpService;
            _validator = validator;
            _usersService = usersService;
            _passwordService = passwordService;
        }
        public override void Configure()
        {
            Get("forgotpassword");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(forgotPasswordRequest);
            if (result.IsValid)
            {
                bool isUser = await _usersService.CheckUsername(forgotPasswordRequest.Username);
                if (isUser == false) 
                {
                    await Send.NotFoundAsync(cancellationToken);
                }

                string totpKey = await _usersService.GetTOTPSecretKey(forgotPasswordRequest.Username, cancellationToken);
                if (string.IsNullOrEmpty(totpKey))
                {
                    throw new Exception("We're sorry, but recovering your password is not possible!");
                }

                bool checkCodes = _totpService.VerifyTOTPCode(totpKey, forgotPasswordRequest.TOTPCode);
                if (checkCodes == false) 
                {
                    throw new Exception("Invalid authentificator code!");
                }

                await _passwordService.ModifyPasswordByUsername(forgotPasswordRequest.Username, forgotPasswordRequest.NewPassword, cancellationToken);
                await Send.OkAsync(result);
            }
            else
            {
                await Send.NoContentAsync(cancellationToken);
            }
        }
    }
}
