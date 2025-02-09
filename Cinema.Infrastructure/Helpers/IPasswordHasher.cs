namespace Cinema.Infrastructure.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
