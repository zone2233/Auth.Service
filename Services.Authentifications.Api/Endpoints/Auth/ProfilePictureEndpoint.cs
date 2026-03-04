using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Application.Responses;
using Azure.Core;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    [RequestSizeLimit(1024 * 1024 * 100)]
    public class ProfilePictureEndpoint : Endpoint<ProfilePictureRequest>
    {
        private readonly IValidator<IFormFile> _validator;
        private readonly IProfilePictureService _profilePictureService;
        private readonly IProfilesService _profilesService;
        public ProfilePictureEndpoint(IProfilePictureService profilePictureService, IValidator<IFormFile> validator, IProfilesService profilesService) 
        {
            _profilePictureService = profilePictureService;
            _validator = validator;
            _profilesService = profilesService;
        }

        public override void Configure()
        {
            Post("profilepicture");
            //Claims(JwtRegisteredClaimNames.Sub);
            AllowFileUploads();
        }

        public override async Task HandleAsync(ProfilePictureRequest profilePictureRequest, CancellationToken cancellationToken)
        {
            if(Files.Count > 0)
            {
                var validationResult = await _validator.ValidateAsync(Files[0], cancellationToken);

                if (validationResult.IsValid)
                {
                    string response = await _profilePictureService.UploadProfilePicture(Files[0]);

                    await _profilesService.InsertProfilePictureByUsername(profilePictureRequest.Username, response);
                    await Send.OkAsync(response, cancellationToken);
                }
                else
                {
                    throw new CustomException(string.Join(", ", validationResult.Errors));
                }
            }

           
        }
    }
}
