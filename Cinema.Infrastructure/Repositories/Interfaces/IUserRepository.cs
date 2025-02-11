using Cinema.Infrastructure.Entities;
using System.Threading.Tasks;
namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository:IRepository<User,int>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
    }
}

