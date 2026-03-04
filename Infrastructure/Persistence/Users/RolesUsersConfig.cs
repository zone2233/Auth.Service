using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Users
{
    public class RolesUsersConfig : IEntityTypeConfiguration<RolesUsers>
    {
        public void Configure(EntityTypeBuilder<RolesUsers> builder)
        {
            builder.HasKey(ck => new { ck.UsersUserId, ck.RolesRoleId });

            builder.HasOne(ru => ru.Users).WithMany(u => u.RolesUsers).HasForeignKey(ru => ru.UsersUserId);

            builder.HasOne(ru => ru.Roles).WithMany(r => r.RolesUsers).HasForeignKey(ru => ru.RolesRoleId);

        }
    }
}
