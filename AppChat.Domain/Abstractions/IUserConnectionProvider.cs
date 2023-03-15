namespace AppChat.Domain.Abstractions
{
    public interface IUserConnectionProvider
    {
        public string this[string userName] { get; }
        string GetConnection(string userName);
        string GetUser(string connection);
        void UpdateConnection(string userName, string connectionId);
        string? RemoveConnection(string connectionId);
    }
}
