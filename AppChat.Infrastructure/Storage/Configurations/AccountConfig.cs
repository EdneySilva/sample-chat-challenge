using AppChat.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppChat.Infrastructure.Storage.Configurations
{
    internal class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(p => p.UserName);
            builder.Property(p => p.UserName);
            builder.Property(p => p.Password);
            builder.Property(p => p.Name).IsRequired(false);
            builder.Property(p => p.Email).IsRequired(false);
            builder.Property(p => p.Thumbnail).IsRequired(false);
            builder.Property(p => p.PhoneNumber).IsRequired(false);
            builder.Property(p => p.CurrentConnectionId).IsRequired(false);
            builder.Property(p => p.Status).HasDefaultValue(ConnectionStatus.Offline);
            builder.Ignore(p => p.Events);
            builder.HasMany(p => p.Rooms).WithMany(p => p.Users).UsingEntity("UsersByRoom");
        }
    }
}