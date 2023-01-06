using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class GroupUserConfiguration : BaseEntityConfiguration<GroupUser>
    {
        public override void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            base.Configure(builder);

            builder.ToTable("GroupUser", WhatsAppWebContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.User)
                .WithMany(x => x.GroupUsers)
                .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Group)
                .WithMany(x => x.GroupUsers)
                .HasForeignKey(x => x.GroupId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserId, x.GroupId }).IsUnique();
        }
    }
}
