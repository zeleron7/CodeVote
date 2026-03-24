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
using CodeVote.src.Utils;

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
            try
            {
                _logger.LogInformation("Fetching all users");

                var users = await _userService.GetAllUsersAsync();
                if (users == null)
                    return NotFound();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all users");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        // GET: CodeVote/User/{id}
        #region GetOneUser
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDTO>> GetOneUser(Guid id)
        {
            try
            { 
                _logger.LogInformation("Fetching user with ID: {id}", id);

                // Retrieve the user ID from claims and pass it to the service layer for authorization check
                var userId = RetrieveUserId.GetUserId(User);

                var user = await _userService.GetUserByIdAsync(id, userId);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user with ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        // PATCH: CodeVote/User/Update/{id}
        #region UpdateUser
        [Authorize]
        [HttpPatch("Update/{id}")] 
        public async Task<ActionResult<ReadUserDTO>> UpdateUser(Guid id, UpdateUserDTO updateUserDto)
        {
            try
            {
                _logger.LogInformation("Updating user with ID: {id}", id);

                var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
                if (updatedUser == null)
                    return NotFound();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        //DELETE: CodeVote/User/Delete/{id}
        #region DeleteUser
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting user with ID: {id}", id);

                // Retrieve the user ID from claims and pass it to the service layer for authorization check
                var userId = RetrieveUserId.GetUserId(User);

                var success = await _userService.DeleteUserAsync(id, userId);
                if (!success)
                    return NotFound();

                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion 
    }
}
