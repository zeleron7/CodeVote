using CodeVote.Data;
using CodeVote.src.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeVote.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDatabaseController : ControllerBase
    {
        private readonly ISeedDatabaseService _seedDatabaseService;
        private readonly ILogger<SeedDatabaseController> _logger;

        public SeedDatabaseController(ISeedDatabaseService seedDatabaseService, ILogger<SeedDatabaseController> logger)
        {
            _seedDatabaseService = seedDatabaseService;
            _logger = logger;
        }

        // POST: CodeVote/SeedDatabase/Seed
        #region SeedDatabase
        [Authorize]
        [HttpPost("Seed")]
        public async Task<IActionResult> SeedDatabase(CodeVoteContext context)
        {
            try
            {
                await _seedDatabaseService.SeedDatabaseAsync(context);
                _logger.LogInformation("Database seeded successfully");
                return Ok("Database seeded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding database");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        // DELETE: CodeVote/SeedDatabase/Clear
        #region ClearDatabase
        [Authorize]
        [HttpDelete("Clear")]
        public async Task<IActionResult> ClearDatabase(CodeVoteContext context)
        {
            try
            {
                await _seedDatabaseService.ClearDatabaseAsync(context);
                _logger.LogInformation("Database cleared successfully");
                return Ok("Database cleared successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing database");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
    }
}
