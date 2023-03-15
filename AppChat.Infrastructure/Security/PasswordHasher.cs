namespace AppChat.Infrastructure.Security
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }
    }
}
