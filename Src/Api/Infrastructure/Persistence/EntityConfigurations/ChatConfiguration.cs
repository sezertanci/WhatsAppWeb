using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Context;

namespace Persistence.EntityConfigurations
{
    public class ChatConfiguration : BaseEntityConfiguration<Chat>
    {
        public override void Configure(EntityTypeBuilder<Chat> builder)
        {
            base.Configure(builder);

            builder.ToTable("Chat", WhatsAppWebContext.DEFAULT_SCHEMA);
        }
    }
}
