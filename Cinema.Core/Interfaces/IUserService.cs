using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
   public interface IUserService
   {
       Task<IEnumerable<UserDTO>> GetAllUsersAsync();
       Task<UserDTO?>GetUserByIdAsync(int id);
       Task AddUserAsync(UserDTO userDto);
       Task<bool> DeleteUserAsync(int id);
   } 
}

