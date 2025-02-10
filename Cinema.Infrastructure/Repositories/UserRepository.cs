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

         public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoleAssignments)
                .ThenInclude(ura => ura.UserRole) 
                .FirstOrDefaultAsync(u => u.UserID == id);
        }

    }
}

