using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Infrastructure.Entities;

namespace Cinema.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDTO loginDto);
        Task<User?> RegisterAsync(CreateUserDTO createUserDto);
        bool UserHasRole(string email, string roleName);
    }
}
