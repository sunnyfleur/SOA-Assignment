using Microsoft.AspNetCore.Mvc;
using SOA_Assignment.Models;
using SOA_Assignment.Common;

namespace SOA_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] ApplicationUser user)
        {
            // Giả sử bạn đã lấy user từ database với password hash
            ApplicationUser storedUser = GetUserFromDatabase(user.Username); // Giả lập

            if (storedUser != null && PasswordHasher.VerifyPassword(user.Password, storedUser.PasswordHash))
            {
                var token = JwtManager.GenerateToken(storedUser, "your_secret_key_here");
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        private ApplicationUser GetUserFromDatabase(string username)
        {
            // Ví dụ giả lập: lấy người dùng có Username = 'admin' và mật khẩu đã băm
            return new ApplicationUser { Username = "admin", PasswordHash = PasswordHasher.HashPassword("password") };
        }
        public void RegisterUser(ApplicationUser user)
        {
            user.PasswordHash = PasswordHasher.HashPassword(user.Password);
            // Lưu user cùng với mật khẩu đã băm vào cơ sở dữ liệu
        }
    }
}
