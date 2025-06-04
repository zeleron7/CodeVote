using CodeVote.Data;
using CodeVote.DbModels;
using CodeVote.DTO;
using CodeVote.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly CodeVoteContext _context;
        private readonly IPasswordHasher<UserDbM> _passwordHasher;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, CodeVoteContext context, IPasswordHasher<UserDbM> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // POST: CodeVote/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            try
            {
                var userLogin = await _context.Users.SingleOrDefaultAsync(u => u.UserName == user.UserName); 
                if (userLogin == null)
                {
                    _logger.LogWarning("Login failed: user not found for username {UserName}", user.UserName);
                    return Unauthorized("Invalid username.");
                }

                var result = _passwordHasher.VerifyHashedPassword(userLogin, userLogin.PasswordHash, user.Password);

                if (result != PasswordVerificationResult.Success)
                {
                    _logger.LogWarning("Login failed: incorrect password for username {UserName}", user.UserName);
                    return Unauthorized("Invalid password.");
                }

                var token = GenerateJwtToken(userLogin); 

                return Ok(new { token });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {UserName}", user.UserName);
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateJwtToken(UserDbM userLogin) 
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userLogin.UserName),
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
