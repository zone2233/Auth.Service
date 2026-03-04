using Application.Interfaces;
using Infrastructure.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentValidation;
using Application.Features.Auth.Payloads.Requests;
using System.Data.Entity.Infrastructure.Design;
using System.Text.Json;
using Application.Responses;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class LoginEndpoint : Endpoint<LoginRequest, BaseResponse<string>>
    {
        private readonly ILoginService _loginService;
        private readonly IValidator<LoginRequest> _validator;

        public LoginEndpoint(ILoginService loginService, IValidator<LoginRequest> validator) 
        {
            _loginService = loginService;
            _validator = validator;

        }

        public override void Configure()
        {
            Post("login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(loginRequest);
            if (result.IsValid) 
            { 
                BaseResponse<string> result2 = await _loginService.Login(loginRequest, cancellationToken);
                
                if(result2.Success)
                {
                    await Send.OkAsync(result2, cancellationToken);
                } else
                {
                    throw new CustomException("Wrong username or password!");
                }
            }else
            {
                throw new CustomException(string.Join(";", result.Errors));
            }
        }
    }
}
