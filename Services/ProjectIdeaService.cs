using AutoMapper;
using CodeVote.Data;
using CodeVote.DTO;
using CodeVote.DbModels;
using Microsoft.EntityFrameworkCore;
using CodeVote.Models;
using System.Linq.Expressions;

namespace CodeVote.Services
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
        public async Task<ReadProjectIdeaDTO> CreateProjectIdeaAsync(CreateProjectIdeaDTO projectIdea)
        {
            try
            {
                if (projectIdea == null)
                {
                    _logger.LogError("CreateProjectIdeaAsync: projectIdea is null");
                    return null;
                }

                var projectIdeaDbM = _mapper.Map<ProjectIdeaDbM>(projectIdea);
                await _context.ProjectIdeas.AddAsync(projectIdeaDbM);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Project idea created successfully ({projectIdeaDbM.ProjectIdeaId})");

                return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaDbM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateProjectIdeaAsync");
                throw;
            }
            
        }

        // Get all project ideas
        public async Task<List<ReadProjectIdeaDTO>> GetAllProjectIdeasAsync()
        {
            try
            {
                var projectIdeaEntities = await _context.ProjectIdeas
                .Include(p => p.VoteDbM)
                .ToListAsync();

                _logger.LogInformation($"Retrieved {projectIdeaEntities.Count} project ideas");

                return _mapper.Map<List<ReadProjectIdeaDTO>>(projectIdeaEntities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllProjectIdeasAsync");
                throw;
            }
        }

        // Get a project idea by ID
        public async Task<ReadProjectIdeaDTO> GetProjectIdeaByIdAsync(Guid projectIdeaId)
        {
            try
            {
                if (projectIdeaId == Guid.Empty)
                {
                    _logger.LogError("GetProjectIdeaByIdAsync: projectIdeaId is empty");
                    return null;
                }
                var projectIdeaEntity = await _context.ProjectIdeas
                .Include(p => p.VoteDbM)
                .FirstOrDefaultAsync(p => p.ProjectIdeaId == projectIdeaId);

                if (projectIdeaEntity == null)
                {
                    _logger.LogError("GetProjectIdeaByIdAsync: projectIdeaEntity is null");
                    return null;
                }

                _logger.LogInformation($"Project idea retrieved successfully ({projectIdeaEntity.ProjectIdeaId})");

                return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProjectIdeaByIdAsync");
                throw;
            }

        }

        // Update a project idea
        public async Task<ReadProjectIdeaDTO> UpdateProjectIdeaAsync(Guid projectIdeaId, UpdateProjectIdeaDTO updateProjectIdeaDto)
        {
            try
            {
                var projectIdeaEntity = await _context.ProjectIdeas.FindAsync(projectIdeaId);
                if (projectIdeaEntity == null)
                {
                    _logger.LogError("UpdateProjectIdeaAsync: projectIdeaEntity is null");
                    return null;
                }

                _mapper.Map(updateProjectIdeaDto, projectIdeaEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Project idea updated successfully ({projectIdeaEntity.ProjectIdeaId})");

                return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaEntity);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error in UpdateProjectIdeaAsync");
                throw;
            }
        }

        // Delete a project idea
        public async Task<bool> DeleteProjectIdeaAsync(Guid projectIdeaId)
        {
            try
            {
                var projectIdeaEntity = await _context.ProjectIdeas.FindAsync(projectIdeaId);
                if (projectIdeaEntity == null)
                {
                    _logger.LogError("DeleteProjectIdeaAsync: projectIdeaEntity is null");
                    return false;
                }
                _context.ProjectIdeas.Remove(projectIdeaEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Project idea deleted successfully ({projectIdeaId})");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProjectIdeaAsync");
                throw;
            }
        }
    }
}
