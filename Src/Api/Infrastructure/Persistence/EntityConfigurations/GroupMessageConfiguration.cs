using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class GroupMessageConfiguration : BaseEntityConfiguration<GroupMessage>
    {
        public override void Configure(EntityTypeBuilder<GroupMessage> builder)
        {
            base.Configure(builder);

            builder.ToTable("GroupMessage", WhatsAppWebContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.GroupUser)
               .WithMany(x => x.GroupMessages)
               .HasForeignKey(x => x.GroupUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
