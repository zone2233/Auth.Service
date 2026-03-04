using Application.Interfaces;
using Domain.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IdentityModel.Tokens.Jwt;

namespace Services.Authentifications.Api.Endpoints.Auth
{
    public class AsignRoleEndpoint : Endpoint<RolesUsers>
    {
        private readonly IAdminService _adminService;
        public AsignRoleEndpoint(IAdminService adminService) 
        {
            _adminService = adminService;   
        }

        public override void Configure()
        {
            Put("asign/role");
            Roles("admin");
            Claims(nameof(Users.UserId));
            Claims(JwtRegisteredClaimNames.Sub);
        }

        public override async Task HandleAsync(RolesUsers rolesUsers, CancellationToken cancellationToken)
        {
            await _adminService.AsignRoleByUserId(Guid.Parse(User.FindFirst(nameof(Users.UserId))?.Value), rolesUsers.RolesRoleId, cancellationToken);
            await Send.OkAsync(rolesUsers, cancellationToken);
        }
    }
}
