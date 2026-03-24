using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CodeVote.Data;
using CodeVote.src.DbModels;
using CodeVote.src.DTO;
using CodeVote.src.Services.Interfaces;

namespace CodeVote.src.Services
{
    public class ProjectIdeaService : IProjectIdeaService
    {
        private readonly CodeVoteContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectIdeaService> _logger;

        public ProjectIdeaService(CodeVoteContext context, IMapper mapper, ILogger<ProjectIdeaService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // Create a new project idea
        #region CreateProjectIdeaAsync
        public async Task<ReadProjectIdeaDTO> CreateProjectIdeaAsync(CreateProjectIdeaDTO projectIdea, Guid? userId)
        {
            try
            {
                if (projectIdea == null)
                {
                    _logger.LogWarning("CreateProjectIdeaAsync: projectIdea is null");
                    return null;
                }

                var projectIdeaDbM = _mapper.Map<ProjectIdeaDbM>(projectIdea);

                // Set the UserId of the project idea to the ID of the currently authenticated user
                projectIdeaDbM.UserId = (Guid)userId; 

                await _context.ProjectIdeas.AddAsync(projectIdeaDbM);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Project idea created successfully with ID: {ProjectIdeaId}", projectIdeaDbM.ProjectIdeaId);

                return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaDbM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateProjectIdeaAsync");
                throw;
            }
        }
        #endregion

        // Get all project ideas
        #region GetAllProjectIdeasAsync
        public async Task<List<ReadProjectIdeaDTO>> GetAllProjectIdeasAsync()
        {
            try
            {
                var projectIdeaEntities = await _context.ProjectIdeas
                .Include(p => p.Votes)
                .ToListAsync();

                _logger.LogInformation("Retrieved {Count} project ideas", projectIdeaEntities.Count);

                return _mapper.Map<List<ReadProjectIdeaDTO>>(projectIdeaEntities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllProjectIdeasAsync");
                throw;
            }
        }
        #endregion

        // Get a project idea by ID
        #region GetProjectIdeaByIdAsync
        public async Task<ReadProjectIdeaDTO> GetProjectIdeaByIdAsync(Guid projectIdeaId)
        {
            try
            {
                if (projectIdeaId == Guid.Empty)
                {
                    _logger.LogWarning("GetProjectIdeaByIdAsync: projectIdeaId is empty");
                    return null;
                }

                var projectIdeaEntity = await _context.ProjectIdeas
                .Include(p => p.Votes)
                .FirstOrDefaultAsync(p => p.ProjectIdeaId == projectIdeaId);

                if (projectIdeaEntity == null)
                {
                    _logger.LogWarning("GetProjectIdeaByIdAsync: Project idea with ID {ProjectIdeaId} was not found", projectIdeaId);
                    return null;
                }

                _logger.LogInformation("Project idea retrieved successfully with ID: {ProjectIdeaId}", projectIdeaEntity.ProjectIdeaId);

                return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProjectIdeaByIdAsync");
                throw;
            }
        }
        #endregion

        // Update a project idea
        #region UpdateProjectIdeaAsync
        public async Task<ReadProjectIdeaDTO> UpdateProjectIdeaAsync(Guid projectIdeaId, UpdateProjectIdeaDTO updateProjectIdeaDto, Guid? userId)
        {
            try
            {
                if (projectIdeaId == Guid.Empty)
                {
                    _logger.LogWarning("UpdateProjectIdeaAsync: projectIdeaId is empty");
                    return null;
                }

                var projectIdeaEntity = await _context.ProjectIdeas.FindAsync(projectIdeaId);
                if (projectIdeaEntity == null)
                {
                    _logger.LogWarning("UpdateProjectIdeaAsync: No project idea found with ID {ProjectIdeaId}", projectIdeaId);
                    return null;
                }
                // Check if the user is authorized to update the project idea (i.e., they are the owner of the project idea)
                if (projectIdeaEntity.UserId != userId)
                {
                    _logger.LogWarning("UpdateProjectIdeaAsync: User {UserId} is not authorized to update project idea with ID {ProjectIdeaId}", userId, projectIdeaId);
                    return null; // Or throw an exception if you prefer
                }

                _mapper.Map(updateProjectIdeaDto, projectIdeaEntity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Project idea updated successfully with ID: {ProjectIdeaId}", projectIdeaEntity.ProjectIdeaId);

                return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProjectIdeaAsync");
                throw;
            }
        }
        #endregion

        // Delete a project idea
        #region DeleteProjectIdeaAsync
        public async Task<bool> DeleteProjectIdeaAsync(Guid projectIdeaId, Guid? userId)
        {
            try
            {
                if (projectIdeaId == Guid.Empty)
                {
                    _logger.LogWarning("DeleteProjectIdeaAsync: projectIdeaId is empty");
                    return false;
                }
                var projectIdeaEntity = await _context.ProjectIdeas.FindAsync(projectIdeaId);

                // Check if the user is authorized to delete the project idea (i.e., they are the owner of the project idea)
                if (projectIdeaEntity == null)
                {
                    _logger.LogWarning("DeleteProjectIdeaAsync: Project idea with ID {ProjectIdeaId} was not found", projectIdeaId);
                    return false;
                }
                if (projectIdeaEntity.UserId != userId)
                {
                    _logger.LogWarning("DeleteProjectIdeaAsync: User {UserId} is not authorized to delete project idea with ID {ProjectIdeaId}", userId, projectIdeaId);
                    return false; // Or throw an exception if you prefer
                }

                _context.ProjectIdeas.Remove(projectIdeaEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Project idea deleted successfully with ID: {ProjectIdeaId}", projectIdeaId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProjectIdeaAsync");
                throw;
            }
        }
        #endregion 
    }
}
