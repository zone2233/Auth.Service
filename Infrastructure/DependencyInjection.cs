using Application.Config;
using Application.Interfaces;
using Domain.Enums;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Settings settings)
        {
            _ = services.AddPooledDbContextFactory<UsersContext>(options =>
            {
                options.UseSqlServer(settings.ConnStrings[ConnStrings.LocalDB.ToString()], sqlServerOption =>
                {
                    sqlServerOption.MigrationsAssembly("Services.Authentifications.Api");
                    sqlServerOption.CommandTimeout(600);
                }).EnableServiceProviderCaching(false);
                options.EnableSensitiveDataLogging();
            });

            _ = services.AddTransient<IUsersService, UsersService>();
            _ = services.AddSingleton<IPasswordService, PasswordService>();
            _ = services.AddTransient<IProfilesService, ProfilesService>();
            _ = services.AddTransient<IRegisterService, RegisterService>();
            _ = services.AddTransient<ILoginService, LoginService>();
            _ = services.AddTransient<ITOTPService, TOTPService>();
            _ = services.AddTransient<IJWTService, JWTService>();
            _ = services.AddTransient<IMigration, MigrationService>();
            _ = services.AddTransient<IAdminService, AdminService>();
            _ = services.AddTransient<IRolesUsersService, RolesUsersService>();
            _ = services.AddTransient<ISendEmailService, SendEmailService>();
            _ = services.AddTransient<IResetTokenService, ResetTokenService>();
            _ = services.AddTransient<IProfilePictureService, ProfilePictureService>();

            return services;
        }


    }
}
