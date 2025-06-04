using CodeVote.DbModels;
using CodeVote.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeVote.Controllers
{
    [Route("CodeVote/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // POST: CodeVote/Auth/Login
        [HttpPost("login")]
        public IActionResult Login(LoginUserDto user)
        {
            _logger.LogInformation("Login attempt for user: {UserName}", user.UserName);

            if (user.UserName == "admin" && user.Password == "password")
            {
                _logger.LogInformation("User {UserName} authenticated successfully", user.UserName);
                var token = GenerateJwtToken(user.UserName);
                return Ok(new { token });
            }
            _logger.LogWarning("Authentication failed for user: {UserName}", user.UserName);
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // JWT Key from appsettings.json / user secrets
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
