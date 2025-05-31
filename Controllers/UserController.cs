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

namespace CodeVote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CodeVoteContext _context;
        private readonly IUserService _userService;

        public UserController(CodeVoteContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<List<ReadUserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        // GET: api/User
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ReadUserDTO>>> GetOneUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDTO updateUserDto)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<ReadUserDTO>> CreateUser(CreateUserDTO createUserDto)
        {
            var createdUser = await _userService.CreateUserAsync(createUserDto);
            if (createdUser == null)
                return BadRequest();

            return createdUser;
        }

        //DELETE: api/User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
