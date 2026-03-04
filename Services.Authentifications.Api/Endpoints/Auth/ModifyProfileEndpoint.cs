using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Domain.Entities;
using FastEndpoints;
using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class ModifyProfileEndpoint : Endpoint<ModifyProfileRequest, Profiles>
    {
        private readonly IValidator<ModifyProfileRequest> _validator;
        private readonly IProfilesService _profilesService;
        public ModifyProfileEndpoint(IValidator<ModifyProfileRequest> validator, IProfilesService profilesService)
        {
            _profilesService = profilesService;
            _validator = validator;
        }

        public override void Configure()
        {
            Put("modify/profile/");
            Roles("user");
            Claims(JwtRegisteredClaimNames.Sub);
        }

        public override async Task HandleAsync(ModifyProfileRequest request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (result.IsValid)
            {
                Profiles profile = await _profilesService.ModifyProfileByUsername(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, request, cancellationToken);

                if (profile == null)
                {
                    await Send.NotFoundAsync(cancellationToken);
                    return;
                }
                await Send.OkAsync(profile, cancellationToken);
            }else
            {
                throw new Exception("Invalid profile request!");
            }
        }
    }
}
