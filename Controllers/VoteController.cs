using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeVote.Data;
using CodeVote.DbModels;
using CodeVote.Interfaces;
using CodeVote.Services;
using CodeVote.DTO;
using Serilog;

namespace CodeVote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly CodeVoteContext _context;
        private readonly IVoteService _voteService;
        private readonly ILogger<VoteController> _logger;

        public VoteController(CodeVoteContext context, IVoteService voteService, ILogger<VoteController> logger)
        {
            _context = context;
            _voteService = voteService;
            _logger = logger;
        }

        // POST: api/Vote
        [HttpPost]
        public async Task<ActionResult<ReadVoteDTO>> Vote(CreateVoteDTO createVoteDto)
        {
            _logger.LogInformation("Creating a new vote");

            var createdVote = await _voteService.CreateVoteAsync(createVoteDto);
            if (createdVote == null)
                return BadRequest("Vote could not be created.");

            return Ok(createdVote);
        }

        // DELETE: api/Vote
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVote(Guid id)
        {
            _logger.LogInformation("Deleting vote with ID: {id}", id);

            var success = await _voteService.DeleteVoteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
