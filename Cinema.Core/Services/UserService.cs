using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Helpers;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
   public class UserService : IUserService
   {
       private readonly IUnitOfWork _unitOfWork;
       private readonly IMapper _mapper;
       private readonly IPasswordHasher _passwordHasher;
       private readonly IUserRepository _userRepository;

       public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, IUserRepository userRepository)
       {
           _unitOfWork = unitOfWork;
           _mapper = mapper;
           _passwordHasher = passwordHasher;
           _userRepository = userRepository;
       }

       public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
       {
           var users = await _unitOfWork.Users.GetAllAsync();
           return _mapper.Map<IEnumerable<UserDTO>>(users);
       }

       public async Task<UserDTO?> GetUserByIdAsync(int id)
       {
           var user = await _unitOfWork.Users.GetByIdAsync(id);
           return user == null ? null : _mapper.Map<UserDTO>(user);
       }

       public async Task<UserDTO> AddUserAsync(CreateUserDTO createUserDto)
        {
            string salt;
            var hashedPassword = _passwordHasher.HashPassword(createUserDto.Password, out salt);

            var defaultRole = await _unitOfWork.Context.UserRoles
                .FirstOrDefaultAsync(r => r.RoleName == "User");

            if (defaultRole == null)
            {
                throw new Exception("Default role 'User' not found");
            }

            var user = new User
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                PasswordHash = hashedPassword,
                Salt = salt,
                RegistrationDate = DateTime.UtcNow,
                UserRoleAssignments = new List<UserRoleAssignment>()
            };

            var userRoleAssignment = new UserRoleAssignment
            {
                User = user,     
                UserRole = defaultRole
            };

            user.UserRoleAssignments.Add(userRoleAssignment);
            
            await _unitOfWork.Users.CreateUserAsync(user);
            await _unitOfWork.CompleteAsync();

            return new UserDTO
            {
                Id = user.UserID,
                Email = user.Email,
                Roles = new List<string> { defaultRole.RoleName }
            };
        }

       public async Task<bool> DeleteUserAsync(int id)
       {
           var user = await _unitOfWork.Users.GetByIdAsync(id);
           if (user == null) return false;

           _unitOfWork.Users.Remove(user);
           await _unitOfWork.CompleteAsync();
           return true;
       }
   }
}
