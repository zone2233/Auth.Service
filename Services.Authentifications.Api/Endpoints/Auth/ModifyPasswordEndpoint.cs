using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using FastEndpoints;
using FluentValidation;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class ModifyPasswordEndpoint : Endpoint<ModifyPasswordRequest>
    {
        private readonly IValidator<ModifyPasswordRequest> _validator;
        private readonly IPasswordService _passwordService;
        public ModifyPasswordEndpoint(IValidator<ModifyPasswordRequest> validator, IPasswordService passwordService) 
        { 
            _passwordService = passwordService;
            _validator = validator;
        }
        public override void Configure()
        {
            Put("modify/password/");
            Roles("user", "admin");
        }

        public override async Task HandleAsync(ModifyPasswordRequest modifyPasswordRequest, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(modifyPasswordRequest);
            if (result.IsValid)
            {
                await _passwordService.ModifyPasswordByUsername(modifyPasswordRequest.Username, modifyPasswordRequest.NewPassword, cancellationToken, modifyPasswordRequest.OldPassword);
                await Send.OkAsync(cancellationToken);
            }
            else
            {
                await Send.NoContentAsync(cancellationToken);
            }
        }
    }
}
