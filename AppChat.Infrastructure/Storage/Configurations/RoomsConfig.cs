using AppChat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppChat.Infrastructure.Storage.Configurations
{
    internal class RoomsConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);
            builder.Property(p => p.DisplayName);
            builder.Property(p => p.Thumbnail);
            builder.HasMany(p => p.Users).WithMany(p => p.Rooms).UsingEntity("UsersByRoom");
            builder.Ignore(p => p.Events);
            builder.Ignore(p => p.DynamicDisplayName);
            builder.HasMany(p => p.Threads).WithOne(p => p.ChatRoom).HasPrincipalKey(p => p.Id);
        }
    }
}
