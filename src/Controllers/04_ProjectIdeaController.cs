using CodeVote.Data;
using CodeVote.src.DTO;
using CodeVote.src.Services.Interfaces;
using CodeVote.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeVote.src.Controllers
{
    [Route("CodeVote/[controller]")]
    [ApiController]
    public class ProjectIdeaController : ControllerBase
    {
        //private readonly CodeVoteContext _context;
        private readonly IProjectIdeaService _projectIdeaService;
        private readonly ILogger<ProjectIdeaController> _logger;

        public ProjectIdeaController(CodeVoteContext context, IProjectIdeaService projectIdeaService, ILogger<ProjectIdeaController> logger)
        {
            //_context = context;
            _projectIdeaService = projectIdeaService;
            _logger = logger;
        }

        // POST: CodeVote/ProjectIdea/Create
        #region CreateProjectIdea
        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<ReadProjectIdeaDTO>> CreateProjectIdea(CreateProjectIdeaDTO createProjectIdea)
        {
            _logger.LogInformation("Creating a new project idea");

            var userId = RetrieveUserId.GetUserId(User);

            var createdProjectIdea = await _projectIdeaService.CreateProjectIdeaAsync(createProjectIdea, userId);
            if (createdProjectIdea == null)
                return BadRequest("Could not create project idea");

            return createdProjectIdea;
        }
        #endregion

        // GET: CodeVote/ProjectIdea/GetAll
        #region GetProjectIdeas
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ReadProjectIdeaDTO>>> GetProjectIdeas()
        {
            _logger.LogInformation("Fetching all project ideas");

            var projectIdeas = await _projectIdeaService.GetAllProjectIdeasAsync();
            if (projectIdeas == null)
                return NotFound();

            return Ok(projectIdeas);
        }
        #endregion

        // GET: CodeVote/ProjectIdea/{id}
        #region GetOneProjectIdea
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProjectIdeaDTO>> GetOneProjectIdea(Guid id)
        {
            _logger.LogInformation("Fetching project idea with ID: {id}", id);

            var projectidea = await _projectIdeaService.GetProjectIdeaByIdAsync(id);
            if (projectidea == null)
                return NotFound();

            return Ok(projectidea);
        }
        #endregion

        // PATCH: CodeVote/ProjectIdea/Update/{id}
        #region UpdateProjectIdea
        [Authorize]
        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<ReadProjectIdeaDTO>> UpdateProjectIdea(Guid id, UpdateProjectIdeaDTO updateprojectideaDto)
        {
            _logger.LogInformation("Updating project idea with ID: {id}", id);

            var updatedProjectidea = await _projectIdeaService.UpdateProjectIdeaAsync(id, updateprojectideaDto);
            if (updatedProjectidea == null)
                return NotFound();

            return Ok(updatedProjectidea);
        }
        #endregion

        //DELETE: CodeVote/ProjectIdea/Delete/{id}
        #region DeleteProjectIdea
        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProjectIdea(Guid id)
        {
            _logger.LogInformation("Deleting project idea with ID: {id}", id);

            var success = await _projectIdeaService.DeleteProjectIdeaAsync(id);
            if (!success)
                return NotFound();

            return Ok("Project idea deleted successfully");
        }
        #endregion
    }
}
