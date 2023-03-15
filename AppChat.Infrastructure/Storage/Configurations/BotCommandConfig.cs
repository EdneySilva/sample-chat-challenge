using AppChat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppChat.Infrastructure.Storage.Configurations
{
    internal class BotCommandConfig : IEntityTypeConfiguration<ChatBotCommandDescriptor>
    {
        public void Configure(EntityTypeBuilder<ChatBotCommandDescriptor> builder)
        {
            builder.ToTable("BotCommands");
            builder.HasKey(p => p.Command);
            builder.Property(p => p.Command);
            builder.Property(p => p.Data);
            builder.Property(p => p.Type);
        }
    }
}
