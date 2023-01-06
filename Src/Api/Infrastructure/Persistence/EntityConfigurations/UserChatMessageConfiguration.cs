using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class UserChatMessageConfiguration : BaseEntityConfiguration<UserChatMessage>
    {
        public override void Configure(EntityTypeBuilder<UserChatMessage> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserChatMessage", WhatsAppWebContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.UserChat)
                .WithMany(x => x.UserChatMessages)
                .HasForeignKey(x => x.UserChatId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
