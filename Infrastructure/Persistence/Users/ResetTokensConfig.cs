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
    public class ResetTokensConfig : IEntityTypeConfiguration<ResetTokens>
    {
        public void Configure(EntityTypeBuilder<ResetTokens> builder)
        {
            builder.HasKey(u => u.ResetTokensId);
            builder.HasOne(u => u.Users).WithMany(u => u.ResetTokens).HasForeignKey(u => u.UserId);
        }
    }
}
