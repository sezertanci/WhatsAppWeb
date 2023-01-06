using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class GroupConfiguration : BaseEntityConfiguration<Group>
    {
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            base.Configure(builder);

            builder.ToTable("Group", WhatsAppWebContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
