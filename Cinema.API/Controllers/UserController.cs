using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateUserDTO> _createUserValidator;
        private readonly IValidator<UserDTO> _userValidator;

        public UserController(IUserService userService, IValidator<CreateUserDTO> createUserValidator, IValidator<UserDTO> userValidator)
        {
            _userService = userService;
            _createUserValidator = createUserValidator;
            _userValidator = userValidator;
        }

        /// <summary>
        /// Отримати всіх користувачів
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Отримати користувача за ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        /// <summary>
        /// Створити нового користувача
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDto)
        {
            var validationResult = await _createUserValidator.ValidateAsync(createUserDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            await _userService.AddUserAsync(createUserDto);
            return Ok("User created successfully");
        }

        /// <summary>
        /// Видалити користувача за ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound("User not found or cannot be deleted");
            return Ok("User deleted successfully");
        }
    }
}
