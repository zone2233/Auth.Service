using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Users
{
    public class ProfilesConfig : IEntityTypeConfiguration<Profiles>
    {
        public void Configure(EntityTypeBuilder<Profiles> builder)
        {
            builder.HasKey(p => p.ProfileId);

            //builder.HasOne(u => u.Users).WithOne(u => u.Profiles).HasForeignKey<Domain.Entities.Users>(p => p.UserId);
        }
    }
}
