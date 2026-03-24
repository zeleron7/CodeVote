using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeVote.src.DbModels;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using CodeVote.Data;
using CodeVote.src.DTO;
using CodeVote.src.Services.Interfaces;
using CodeVote.src.Utils;

namespace CodeVote.src.Controllers
{
    [Route("CodeVote/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly ILogger<VoteController> _logger;

        public VoteController(CodeVoteContext context, IVoteService voteService, ILogger<VoteController> logger)
        {
            _voteService = voteService;
            _logger = logger;
        }

        // POST: CodeVote/Vote
        #region CreateVote
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReadVoteDTO>> CreateVote(CreateVoteDTO createVoteDto)
        {
            try
            { 
                _logger.LogInformation("Creating a new vote");

                var createdVote = await _voteService.CreateVoteAsync(createVoteDto);
                if (createdVote == null)
                    return BadRequest("Could not create vote");

                return Ok(createdVote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating vote");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion

        // DELETE: CodeVote/Vote/Delete/{id}
        #region DeleteVote
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteVote(Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting vote with ID: {id}", id);

                var success = await _voteService.DeleteVoteAsync(id);
                if (!success)
                    return NotFound();

                return Ok("Vote deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting vote with ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion 
    }
}
