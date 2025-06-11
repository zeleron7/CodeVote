using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeVote.src.DbModels;
using Microsoft.AspNetCore.Authorization;
using CodeVote.Data;
using CodeVote.src.DTO;
using CodeVote.src.Services.Interfaces;

namespace CodeVote.src.Controllers
{
    [Route("CodeVote/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(CodeVoteContext context, IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: CodeVote/User/GetAll
        #region GetAllUsers
        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ReadUserDTO>>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");

            var users = await _userService.GetAllUsersAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }
        #endregion

        // GET: CodeVote/User/{id}
        #region GetOneUser
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDTO>> GetOneUser(Guid id)
        {
            _logger.LogInformation("Fetching user with ID: {id}", id);

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        #endregion

        // PATCH: CodeVote/User/Update/{id}
        #region UpdateUser
        [Authorize]
        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<ReadUserDTO>> UpdateUser(Guid id, UpdateUserDTO updateUserDto)
        {
            _logger.LogInformation("Updating user with ID: {id}", id);

            var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }
        #endregion

        //DELETE: CodeVote/User/Delete/{id}
        #region DeleteUser
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogInformation("Deleting user with ID: {id}", id);

            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound();

            return Ok("User deleted successfully");
        }
        #endregion 
    }
}
