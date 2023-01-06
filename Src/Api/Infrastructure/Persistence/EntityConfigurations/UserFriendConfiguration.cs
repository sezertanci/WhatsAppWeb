using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class UserFriendConfiguration : BaseEntityConfiguration<UserFriend>
    {
        public override void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserFriend", WhatsAppWebContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserFriends)
                .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserId, x.PhoneNumber }).IsUnique();
        }
    }
}
