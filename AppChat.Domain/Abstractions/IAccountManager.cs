namespace AppChat.Domain.Abstractions
{
    public interface IAccountManager
    {
        Task CreateAsync(Account user);
    }
}
