using AppChat.Domain;
using AppChat.Infrastructure.Storage.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics;

namespace AppChat.Infrastructure.Storage
{
    public class FaceRecognitionDbContextContextFactory : IDesignTimeDbContextFactory<AppChatDbContext>
    {
        public AppChatDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppChatDbContext>();
            Debug.WriteLine("ConnectionString: " + args[0]);
            optionsBuilder.UseSqlServer(string.Join(' ', args));
            return new AppChatDbContext(optionsBuilder.Options);
        }
    }

    public class AppChatDbContext : DbContext
    {
        IMediator _mediatorHandler;

        internal AppChatDbContext(IMediator mediatorHandler, DbContextOptions options)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public AppChatDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true).LogTo(p => Console.WriteLine(p));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomsConfig());
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new BotCommandConfig());
            modelBuilder.ApplyConfiguration(new ThreadMessagesConfig());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            foreach (var item in entries)
            {
                var entity = (item.Entity as Entity);
                if(entity == null)
                    continue;
                foreach(var evt in entity.EventsToDispatch())
                    await _mediatorHandler.Publish(evt);
            }
            return result;
        }
    }
}
