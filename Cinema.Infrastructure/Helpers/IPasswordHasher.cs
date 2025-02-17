namespace Cinema.Infrastructure.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, out string salt);
        bool VerifyPassword(string password, string hashedPassword, string salt);
    }
}
