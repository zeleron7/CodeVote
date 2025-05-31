using CodeVote.Data;
using CodeVote.DTO;
using CodeVote.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeVote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectIdeaController : ControllerBase
    {
        private readonly CodeVoteContext _context;
        private readonly IProjectIdeaService _projectIdeaService;
        private readonly ILogger<ProjectIdeaController> _logger;

        public ProjectIdeaController(CodeVoteContext context, IProjectIdeaService projectIdeaService, ILogger<ProjectIdeaController> logger)
        {
            _context = context;
            _projectIdeaService = projectIdeaService;
            _logger = logger;
        }

        // GET: api/ProjectIdea
        [HttpGet]
        public async Task<ActionResult<List<ReadProjectIdeaDTO>>> GetProjectIdeas()
        {
            _logger.LogInformation("Fetching all project ideas");
            var users = await _projectIdeaService.GetAllProjectIdeasAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        // GET: api/ProjectIdea
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ReadProjectIdeaDTO>>> GetOneProjectIdea(Guid id)
        {
            _logger.LogInformation($"Fetching project idea with ID: {id}");
            var projectidea = await _projectIdeaService.GetProjectIdeaByIdAsync(id);
            if (projectidea == null)
                return NotFound();

            return Ok(projectidea);
        }

        // PUT: api/ProjectIdea
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectIdea(Guid id, UpdateProjectIdeaDTO updateprojectideaDto)
        {
            _logger.LogInformation($"Updating project idea with ID: {id}");
            var updatedProjectidea = await _projectIdeaService.UpdateProjectIdeaAsync(id, updateprojectideaDto);
            if (updatedProjectidea == null)
                return NotFound();

            return Ok(updatedProjectidea);
        }

        // POST: api/ProjectIdea
        [HttpPost]
        public async Task<ActionResult<ReadProjectIdeaDTO>> CreateProjectIdea(CreateProjectIdeaDTO createProjectIdea)
        {
            _logger.LogInformation("Creating a new project idea");
            var createdProjectIdea = await _projectIdeaService.CreateProjectIdeaAsync(createProjectIdea);
            if (createdProjectIdea == null)
                return BadRequest();

            return createdProjectIdea;
        }

        //DELETE: api/ProjectIdea
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectIdea(Guid id)
        {
            _logger.LogInformation($"Deleting project idea with ID: {id}");
            var success = await _projectIdeaService.DeleteProjectIdeaAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
