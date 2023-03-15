namespace AppChat.Infrastructure.Security
{
    /// <summary>
    /// used same concept from Asp.Net Identity
    /// </summary>
    internal interface IPasswordHasher
    {
        public string HashPassword(string password);
    }
}
