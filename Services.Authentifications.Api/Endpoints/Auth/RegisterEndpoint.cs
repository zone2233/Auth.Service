using FastEndpoints;
using Application.Interfaces;
using FluentValidation;
using System.Runtime.CompilerServices;
using Application.Features.Auth.Payloads.Requests;
using Application.Responses;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class RegisterEndpoint : Endpoint<RegisterRequest, Guid>
    {
        private readonly IRegisterService _registerService;
        private readonly IValidator<RegisterRequest> _validator;
        public RegisterEndpoint(IRegisterService registerService, IValidator<RegisterRequest> validator) {
            _registerService = registerService;
            _validator = validator;
        }
        
        public override void Configure()
        {
            Post("register");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                Guid? response = await _registerService.Register(request, cancellationToken);
                await Send.OkAsync(response.Value, cancellationToken);
            }
            else {
                throw new CustomException(string.Join(", ",validationResult.Errors));
            }
        }

    }
}
