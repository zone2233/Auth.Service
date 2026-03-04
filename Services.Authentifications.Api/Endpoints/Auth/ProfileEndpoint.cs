using Application.Features.Auth.Payloads;
using Application.Interfaces;
using Domain.Entities;
using FastEndpoints;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class ProfileEndpoint : EndpointWithoutRequest<Profiles>
    {
        private readonly IProfilesService _profileService;

        
        public ProfileEndpoint(IProfilesService profileService)
        {
            _profileService = profileService;
        }

        public override void Configure()
        {
            Get("profile");
            //Roles("users");
            Claims(JwtRegisteredClaimNames.Sub);
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            Profiles profile = await _profileService.GetProfileByUsername(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value);
            await Send.OkAsync(profile, cancellationToken);
        }
    }
}
