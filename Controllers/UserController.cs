using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeVote.Data;
using CodeVote.DbModels;
using CodeVote.Services;
using CodeVote.DTO;
using Microsoft.AspNetCore.Authorization;

namespace CodeVote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CodeVoteContext _context;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(CodeVoteContext context, IUserService userService, ILogger<UserController> logger)
        {
            _context = context;
            _userService = userService;
            _logger = logger;
        }

        // GET: api/User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ReadUserDTO>>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");

            var users = await _userService.GetAllUsersAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        // GET: api/User
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ReadUserDTO>>> GetOneUser(Guid id)
        {
            _logger.LogInformation("Fetching user with ID: {id}", id);

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/User
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDTO updateUserDto)
        {
            _logger.LogInformation("Updating user with ID: {id}", id);

            var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // POST: api/User
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReadUserDTO>> CreateUser(CreateUserDTO createUserDto)
        {
            _logger.LogInformation("Creating a new user");

            var createdUser = await _userService.CreateUserAsync(createUserDto);
            if (createdUser == null)
                return BadRequest("Could not create user");

            return createdUser;
        }

        //DELETE: api/User
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogInformation("Deleting user with ID: {id}", id);

            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
