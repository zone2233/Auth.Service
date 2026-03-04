using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RolesUsersService : IRolesUsersService
    {
        private readonly IDbContextFactory<UsersContext> _contextFactory;
        public RolesUsersService(IDbContextFactory<UsersContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<RolesUsers> AddDefault(Guid userId, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                RolesUsers rolesUsers = new RolesUsers()
                {
                    RolesRoleId = 2,
                    UsersUserId = userId
                };
                await context.RolesUsers.AddAsync(rolesUsers, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return rolesUsers;
            }
        }

        public async Task DeleteRolesUsers(RolesUsers roleUsers, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                context.RolesUsers.Remove(roleUsers);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Roles>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                List<Roles> rolesUsersList = await context.RolesUsers.Include(ru => ru.Roles).Where(ru => ru.UsersUserId == userId).Select(ru => ru.Roles).ToListAsync();
                return rolesUsersList;
            }
        }

        public async Task<RolesUsers> InsertRolesUsers(RolesUsers rolesUsers, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                await context.RolesUsers.AddAsync(rolesUsers, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return rolesUsers;
            }
        }

        public async Task<RolesUsers> UpdateRolesUsers(RolesUsers rolesUsers, int newRoleId, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                RolesUsers updatedRolesUsers = await context.RolesUsers.Where(u => u.RolesRoleId == rolesUsers.RolesRoleId && u.UsersUserId == rolesUsers.UsersUserId).FirstOrDefaultAsync(cancellationToken);
                if (updatedRolesUsers == null)
                {
                    throw new Exception("RolesUsers not found!");
                }
                updatedRolesUsers.RolesRoleId = newRoleId;
                await context.SaveChangesAsync(cancellationToken);
                return updatedRolesUsers;
            }
        }
    }
}
