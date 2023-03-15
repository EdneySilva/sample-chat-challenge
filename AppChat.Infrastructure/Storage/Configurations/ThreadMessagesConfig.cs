using AppChat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppChat.Infrastructure.Storage.Configurations
{
    internal class ThreadMessagesConfig : IEntityTypeConfiguration<ThreadMessage>
    {
        public void Configure(EntityTypeBuilder<ThreadMessage> builder)
        {
            builder.ToTable("Threads");
            builder.HasKey(p => p.MessageId);
            builder.Property(p => p.MessageId);
            builder.Property(p => p.From);
            builder.Property(p => p.To);
            builder.Property(p => p.ContentType);
            builder.Property(p => p.MessageDelivered);
            builder.Property(p => p.Content);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.MessageRead);
            builder.Property(p => p.Room);
            builder.HasOne(p => p.ChatRoom).WithMany(p => p.Threads);
        }
    }
}
