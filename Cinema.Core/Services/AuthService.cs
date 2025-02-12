using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace Cinema.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginDTO loginDto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoleAssignments)
                    .ThenInclude(ura => ura.UserRole) 
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt))
                return null;

            return GenerateJwtToken(user);
        }

       public async Task<User?> RegisterAsync(CreateUserDTO createUserDto)
        {
            string salt;
            var hashedPassword = _passwordHasher.HashPassword(createUserDto.Password, out salt);

            var newUser = new User
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                PasswordHash = hashedPassword,
                Salt = salt,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",", user.UserRoleAssignments.Select(r => r.UserRole.RoleName)))
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool UserHasRole(string email, string roleName)
        {
            var user = _context.Users
                .Include(u => u.UserRoleAssignments)
                    .ThenInclude(ura => ura.UserRole)
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
                return false;

            return user.UserRoleAssignments
                .Any(ura => ura.UserRole.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
