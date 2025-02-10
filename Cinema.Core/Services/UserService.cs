using AutoMapper;
using Cinema.Infrastructure.Helpers;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
   public class UserService: IUserService
   {
       private readonly IUnitOfWork _unitOfWork;
       private readonly IMapper _mapper;
       private readonly IPasswordHasher _passwordHasher;
       public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
       {
           _unitOfWork = unitOfWork;
           _mapper = mapper;
           _passwordHasher = passwordHasher;
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
        
        var user = new User
        {
            Email = createUserDto.Email,
            PasswordHash = hashedPassword,
            Salt = salt,
            UserRoleAssignments = new List<UserRoleAssignment>()
        };

        var defaultRole = await _roleRepository.GetRoleByNameAsync("User");
        if (defaultRole == null)
        {
            throw new Exception("Default role 'User' not found");
        }
        
        user.UserRoleAssignments.Add(new UserRoleAssignment
        {
            UserId = user.UserID,
            RoleId = defaultRole.RoleID,
        });

        await _userRepository.CreateUserAsync(user);
        await _unitOfWork.CompleteAsync();

        return new UserDTO
        {
            Id = user.Id,
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

