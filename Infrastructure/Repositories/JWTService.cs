using Application.Config;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class JWTService : IJWTService
    {
        private readonly ITOTPService _tOTPService;
        private readonly Settings _settings;
        private readonly IRolesUsersService _rolesUsersService;
        private readonly IUsersService _usersService;
        public JWTService(IRolesUsersService rolesUsersService, IUsersService usersService, IOptions<Settings> setting,
            ITOTPService tOTPService)
        {
            _rolesUsersService = rolesUsersService;
            _usersService = usersService;
            _settings = setting.Value;
            _tOTPService = tOTPService;
        }
        /*public async Task<string> GenerateLoginJWTToken(string username)
        {
            Domain.Entities.Users user =await _usersService.GetUserByUsername(username);

            var jwtToken = JwtBearer.CreateToken(o => {

                o.SigningKey = _settings.JWTSettings["Secret"];
                o.ExpireAt = DateTime.UtcNow.AddMinutes(30);
                o.User.Claims.Add((JwtRegisteredClaimNames.Sub, username));
                o.User.Claims.Add((nameof(Users.UserId), user.UserId.ToString()));
            });
            return jwtToken;
        }*/

        /*public async Task<string> GenerateFirstAuthJWTToken(string username)
        {
            Domain.Entities.Users user = await _usersService.GetUserByUsername(username);
            Tuple<string, string> qr = _tOTPService.GenerateQRCode(username);

            var jwtToken = JwtBearer.CreateToken(o => {

                o.SigningKey = _settings.JWTSettings["Secret"];
                o.ExpireAt = DateTime.UtcNow.AddMinutes(30);
                o.User.Claims.Add((JwtRegisteredClaimNames.Sub, username));
                o.User.Claims.Add((nameof(Users.UserId), user.UserId.ToString()));

                o.User.Claims.Add(("TOTPSecret", qr.Item1));
                o.User.Claims.Add(("qr", qr.Item2));
            });
            return jwtToken;
        }*/

        public async Task<string> GenerateAuthJWTToken(string username, AuthentificationState authentificationState)
        {
            Domain.Entities.Users user = await _usersService.GetUserByUsername(username);

            List<Roles> roles = await _rolesUsersService.GetRolesByUserId(user.UserId);

            var jwtToken = JwtBearer.CreateToken(o => {

                Tuple<string, string> qr = _tOTPService.GenerateQRCode(username);

                switch (authentificationState)
                {
                    case AuthentificationState.QrAndSecretKey:
                        o.User.Claims.Add(("TOTPSecret", qr.Item1));
                        o.User.Claims.Add(("qr", qr.Item2));
                        break;
                    case AuthentificationState.TOTPCode:
                        break;
                    case AuthentificationState.Checked:
                        foreach (Roles role in roles)
                        {
                            o.User.Roles.Add(role.RoleName);
                        }
                        break;

                }
                o.User.Claims.Add((nameof(AuthentificationState), authentificationState.ToString()));

                o.SigningKey = _settings.JWTSettings["Secret"];
                o.ExpireAt = DateTime.UtcNow.AddMinutes(30);
                o.User.Claims.Add((JwtRegisteredClaimNames.Sub, username));
                o.User.Claims.Add((nameof(Users.UserId), user.UserId.ToString()));
            });
            return jwtToken;
        }
    }
}
