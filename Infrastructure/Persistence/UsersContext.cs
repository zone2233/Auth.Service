using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UsersContext (DbContextOptions<UsersContext> options) : DbContext(options)
    {
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Domain.Entities.Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesUsers> RolesUsers { get; set; }
        public DbSet<ResetTokens> ResetTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Domain.Entities.Users>().HasOne(u => u.Profiles).WithOne(u => u.Users).HasForeignKey<Domain.Entities.Profiles>(u => u.UserId);
        }
    }
}
