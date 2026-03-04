using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RegisterService : IRegisterService
    {
        private readonly IRolesUsersService _rolesUsersService;
        private readonly IPasswordService _passwordService;
        private readonly IProfilesService _profilesService;
        private readonly IUsersService _usersService;
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> _contextFactory; 
        public RegisterService(IRolesUsersService rolesUsersService, IProfilesService profilesService, IUsersService usersService, Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory,
                  IPasswordService passwordService)
        {
            _rolesUsersService = rolesUsersService;
            _passwordService = passwordService;
            _contextFactory = contextFactory;
            _profilesService = profilesService;
            _usersService = usersService;
        }
        public async Task<Guid?> Register(RegisterRequest registerRequest, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                using (IDbContextTransaction UsersTransaction = await context.Database.BeginTransactionAsync(cancellationToken))
                {
                    try
                    {
                        Roles role = context.Roles.Where(r => r.RoleId == 1).SingleOrDefault();

                        Users user = new Users()
                        {
                            UserId = Guid.NewGuid(),
                            Username = registerRequest.Username,
                            CreationTime = registerRequest.CreationTime,
                            Password = _passwordService.HashPassword(registerRequest.Password),
                            //Roles = new List<Roles>()
                        };

                        user = await _usersService.InsertUser(user, cancellationToken);
                        //user.Roles.Add(role);
                        await _rolesUsersService.AddDefault(user.UserId, cancellationToken);
                        
                        Profiles profile = new Profiles()
                        {
                            FirstName = registerRequest.FirstName,
                            LastName = registerRequest.LastName,
                            UserId = user.UserId,
                            Address = registerRequest.Address,
                            Email = registerRequest.Email,
                            PhoneNumber = registerRequest.PhoneNumber,
                            ProfileId = Guid.NewGuid()
                        };

                        profile = await _profilesService.InsertProfile(profile, cancellationToken);

                        await UsersTransaction.CommitAsync(cancellationToken);

                        return user.UserId;
                    }catch
                    {
                        await UsersTransaction.RollbackAsync(cancellationToken);
                        return null;
                    }
                }
            }
        }
    }
}
