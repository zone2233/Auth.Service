using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using FastEndpoints;
using FluentValidation;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class TOTPEndpoint : Endpoint<TOTPCodeRequest, BaseResponse<string>>
    {
        private readonly ITOTPService _totpService;
        private readonly IUsersService _usersService;
        private readonly IJWTService _jWTService;
        private readonly IValidator<TOTPCodeRequest> _validator;

        public TOTPEndpoint(ITOTPService totpService, IUsersService usersService, IJWTService jWTService, IValidator<TOTPCodeRequest> validator)
        {
            _validator = validator;
            _totpService = totpService;
            _usersService = usersService;
            _jWTService = jWTService;
        }

        public override void Configure()
        {
            Post("auth");
            //AllowAnonymous();
            Claims(JwtRegisteredClaimNames.Sub);

        }
        public override async Task HandleAsync(TOTPCodeRequest tOTPCodeRequest, CancellationToken cancellationToken)
        {
            var isRequestValid = _validator.Validate(tOTPCodeRequest);
            if (isRequestValid.IsValid)
            {
                string secretKey = await _usersService.GetTOTPSecretKey(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, cancellationToken);
                bool isValid;
                string jwtSecretTOTP = User.FindFirst("TOTPSecret")?.Value;
                if (string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(jwtSecretTOTP))
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }

                bool checkCodes = _totpService.VerifyTOTPCode(isValid ? jwtSecretTOTP : secretKey, tOTPCodeRequest.TOTPCode);
                if (checkCodes)
                {
                    if (isValid)
                    {
                        _usersService.InsertTOTPSecret(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, jwtSecretTOTP, cancellationToken);
                    }
                    string jwt = await _jWTService.GenerateAuthJWTToken(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, Domain.Enums.AuthentificationState.Checked);

                    BaseResponse<string> result = new BaseResponse<string>
                    {
                        Success = true,
                        Message = "Success",
                        Data = jwt
                    };

                    await Send.OkAsync(result, cancellationToken);
                }
                else
                {
                    throw new CustomException("Wrong code! Try again!");
                }
            }
            else
            {
                throw new CustomException("Code must be 6 characters long and only contain numbers!");
            }
        }
    }
}
