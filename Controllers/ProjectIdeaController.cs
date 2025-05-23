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

        public ProjectIdeaController(CodeVoteContext context, IProjectIdeaService projectIdeaService)
        {
            _context = context;
            _projectIdeaService = projectIdeaService;
        }

        // GET: api/ProjectIdea
        [HttpGet]
        public async Task<ActionResult<List<ReadProjectIdeaDTO>>> GetProjectIdeas()
        {
            var users = await _projectIdeaService.GetAllProjectIdeasAsync();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        // GET: api/ProjectIdea
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ReadProjectIdeaDTO>>> GetOneProjectIdea(Guid id)
        {
            var projectidea = await _projectIdeaService.GetProjectIdeaByIdAsync(id);
            if (projectidea == null)
                return NotFound();

            return Ok(projectidea);
        }

        // PUT: api/ProjectIdea
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectIdea(Guid id, UpdateProjectIdeaDTO updateprojectideaDto)
        {
            var updatedProjectidea = await _projectIdeaService.UpdateProjectIdeaAsync(id, updateprojectideaDto);
            if (updatedProjectidea == null)
                return NotFound();

            return Ok(updatedProjectidea);
        }

        // POST: api/ProjectIdea
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReadProjectIdeaDTO>> CreateProjectIdea(CreateProjectIdeaDTO createProjectIdea)
        {
            var createdProjectIdea = await _projectIdeaService.CreateProjectIdeaAsync(createProjectIdea);
            if (createdProjectIdea == null)
                return BadRequest();

            return createdProjectIdea;
        }

        //DELETE: api/ProjectIdea
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectIdea(Guid id)
        {
            var success = await _projectIdeaService.DeleteProjectIdeaAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
