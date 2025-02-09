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

       public async Task AddUserAsync(CreateUserDTO createUserDto)
       {
            var hashedPassword = _passwordHasher.HashPassword(createUserDto.Password);
           var user = _mapper.Map<User>(createUserDto);
           user.PasswordHash = hashedPassword;
           await _unitOfWork.Users.AddAsync(user);
           await _unitOfWork.CompleteAsync();
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

