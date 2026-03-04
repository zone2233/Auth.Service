using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using FastEndpoints;
using FluentValidation;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class ThumbnailEndpoint : Endpoint<ThumbnailRequest>
    {
        private readonly IProfilesService _profilesService;
        private readonly IValidator<ThumbnailRequest> _validator;
        public ThumbnailEndpoint(IProfilesService profilesService, IValidator<ThumbnailRequest> validator) 
        { 
            _validator = validator;
            _profilesService = profilesService;
        }

        public override void Configure()
        {
            Get("thumbnail");
        }

        public override async Task HandleAsync(ThumbnailRequest thumbnailRequest, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(thumbnailRequest);
            if(result.IsValid)
            {

            }
        }
    }
}
