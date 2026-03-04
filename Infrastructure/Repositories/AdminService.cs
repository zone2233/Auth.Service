using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminService : IAdminService
    {
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> _contextFactory;
        private readonly IUsersService _usersService;
        private readonly IRolesUsersService _rolesUsersService;
        public AdminService(Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory, IUsersService usersService, IRolesUsersService rolesUsersService)
        {
            _contextFactory = contextFactory;
            _usersService = usersService;
            _rolesUsersService = rolesUsersService;
        }

        public async Task<RolesUsers> AsignRoleByUserId(Guid userId, int roleId, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await _usersService.GetUserById(userId, cancellationToken);

                if(!context.Roles.Any(r => r.RoleId == roleId)) 
                {
                    throw new Exception("The roleId was not found!");
                }

                if (user == null) 
                {
                    throw new Exception("User not found!");
                }

                RolesUsers rolesUsers = new RolesUsers()
                {
                    RolesRoleId = roleId,
                    UsersUserId = userId
                };

                if (context.RolesUsers.Any(ru => ru.RolesRoleId == rolesUsers.RolesRoleId &&  ru.UsersUserId == rolesUsers.UsersUserId))
                {
                    throw new Exception("Identical RolesUsers entity was found!");
                }

                await _rolesUsersService.InsertRolesUsers(rolesUsers, cancellationToken);
                return rolesUsers;
            }
        }

        public async Task<RolesUsers> ModifyRoleByUserId(Guid userId, int oldRoleId, int newRoleId, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                RolesUsers rolesUsers = new RolesUsers()
                {
                    RolesRoleId = oldRoleId,
                    UsersUserId = userId
                };
                await _rolesUsersService.UpdateRolesUsers(rolesUsers, newRoleId, cancellationToken);
                return rolesUsers;
            }
        }
    }
}
