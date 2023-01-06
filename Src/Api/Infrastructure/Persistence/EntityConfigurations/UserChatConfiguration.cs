using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class UserChatConfiguration : BaseEntityConfiguration<UserChat>
    {
        public override void Configure(EntityTypeBuilder<UserChat> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserChat", WhatsAppWebContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserChats)
                .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Chat)
                .WithMany(x => x.UserChats)
                .HasForeignKey(x => x.ChatId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserId, x.ChatId }).IsUnique();
        }
    }
}
