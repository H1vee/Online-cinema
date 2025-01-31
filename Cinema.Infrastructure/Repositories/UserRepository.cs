using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class UserRepository:Repository<User, int>,  IUserRepository
    {
        public UserRepository(ApplicationDbContext context):base(context){ }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

