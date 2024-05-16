using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOA_Assignment.Common;
using SOA_Assignment.Models;

namespace SOA_Assignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly YourDbContext _context;
        private const string SecretKey = "your_super_secret_key_here";
        private const string Issuer = "YourIssuer";
        private const string Audience = "YourAudience";

        public AccountController(YourDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var newEmployee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Username = model.Username,
                Password = model.Password
            };

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _context.Employees
                                     .FirstOrDefaultAsync(u => u.Username == loginModel.Username);

            if (user == null || !VerifyPassword(loginModel.Password, user.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = JwtManager.GenerateToken(user, SecretKey, Issuer, Audience);

            return Ok(new { Token = token });
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return inputPassword == storedPassword;
        }
    }
}
