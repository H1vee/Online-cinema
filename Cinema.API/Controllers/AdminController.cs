using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Cinema.Infrastructure.Data;
using System.Linq;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger; 

        public AdminController(IAuthService authService, ApplicationDbContext context, ILogger<AdminController> logger)
        {
            _authService = authService;
            _context = context;
            _logger = logger;
        }

        [HttpGet("dashboard")]
        public IActionResult GetAdminDashboard()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            
            _logger.LogInformation("Getting user with email: {UserEmail}", userEmail);

            if (string.IsNullOrEmpty(userEmail))
            {
                _logger.LogWarning("No email found in token for current user");
                return Forbid();
            }

            var user = _context.Users
                .Include(u => u.UserRoleAssignments)
                .ThenInclude(ura => ura.UserRole)
                .FirstOrDefault(u => u.Email.ToLower() == userEmail.ToLower());

            if (user == null)
            {
                _logger.LogWarning("User not found with email: {UserEmail}", userEmail);
                return Forbid();
            }

            var hasAdminRole = user.UserRoleAssignments.Any(ura => ura.UserRole.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase));

            if (!hasAdminRole)
            {
                _logger.LogWarning("User does not have Admin role: {UserEmail}", userEmail);
                return Forbid();
            }
            return Ok("Welcome, Admin!");
        }
    }
}
