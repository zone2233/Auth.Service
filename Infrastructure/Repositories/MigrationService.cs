using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MigrationService(Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory, IHostApplicationLifetime applicationLifetime) : IMigration
    {
        private readonly CancellationToken cancellationToken = applicationLifetime.ApplicationStopping;

        public async Task CreateTables()
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                await context.Database.MigrateAsync(cancellationToken);
            }
        }

        public async Task InsertRoles()
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Roles[] roles = new[]
                {
                     new Roles()
                    {
                        RoleName = "user"
                    }, 
                    new Roles()
                    {
                        RoleName = "admin"
                    },
                    new Roles()
                    {
                        RoleName = "medic"
                    },
                    new Roles()
                    {
                        RoleName = "receptionist"
                    },
                    new Roles()
                    {
                        RoleName = "auditor"
                    }
                };

                Roles[] existingRoles = await context.Roles.ToArrayAsync(cancellationToken);

                Roles[] toAddRoles = roles.Where(r => !existingRoles.Any(ro => ro.RoleName == r.RoleName)).ToArray();

                if (toAddRoles.Any())
                {
                    await context.Roles.AddRangeAsync(toAddRoles);
                    await context.SaveChangesAsync();
                }
                
            }
        }
    }
}
