using AppChat.Domain;
using AppChat.Domain.Abstractions;
using AppChat.Infrastructure.Security;

namespace AppChat.Infrastructure.Storage
{
    internal class UserManager : IAccountManager
    {
        private readonly AppChatDbContext _appChatDbContext;
        private readonly IPasswordHasher _passwordHasher;

        public UserManager(AppChatDbContext appChatDbContext, IPasswordHasher passwordHasher)
        {
            _appChatDbContext = appChatDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task CreateAsync(Account account)
        {
            var newPwd = _passwordHasher.HashPassword(account.Password);
            account.Password = newPwd;
            await _appChatDbContext.AddAsync(account);
            await _appChatDbContext.SaveChangesAsync();
        }
    }
}
