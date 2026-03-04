using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Users
{
    public class UsersConfig : IEntityTypeConfiguration<Domain.Entities.Users>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Users> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.HasMany(u => u.ResetTokens).WithOne(u => u.Users);
        }
    }
}
