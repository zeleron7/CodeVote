using AutoMapper;
using CodeVote.Data;
using CodeVote.DTO;
using CodeVote.DbModels;
using Microsoft.EntityFrameworkCore;
using CodeVote.Models;

namespace CodeVote.Services
{
    public class VoteService : IVoteService
    {
        private readonly CodeVoteContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<VoteService> _logger;

        public VoteService(CodeVoteContext context, IMapper mapper, ILogger<VoteService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // Create a new vote
        public async Task<ReadVoteDTO> CreateVoteAsync(CreateVoteDTO voteDto)
        {
            try
            {
                var userExists = await _context.Users.AnyAsync(u => u.UserId == voteDto.UserId);
                if (!userExists)
                {
                    _logger.LogWarning("User with ID {UserId} does not exist.", voteDto.UserId);
                    return null;
                }
                    
                var project = await _context.ProjectIdeas
                .FirstOrDefaultAsync(p => p.ProjectIdeaId == voteDto.ProjectIdeaId);
                if (project == null)
                {
                    _logger.LogWarning("Project idea with ID {ProjectIdeaId} does not exist.", voteDto.ProjectIdeaId);
                    return null;
                }
                
                // Create and save vote
                var vote = new VoteDbM
                {
                    VoteId = Guid.NewGuid(),
                    UserId = voteDto.UserId,
                    ProjectIdeaId = voteDto.ProjectIdeaId
                };

                _context.Votes.Add(vote);
                project.VoteCount += 1;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Vote created successfully ({VoteId}) for project idea ({ProjectIdeaId})", vote.VoteId, project.ProjectIdeaId);

                var savedVote = await _context.Votes
                    .Include(v => v.ProjectIdeaDbM)
                    .FirstOrDefaultAsync(v => v.VoteId == vote.VoteId);

                return _mapper.Map<ReadVoteDTO>(savedVote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateVoteAsync");
                throw;
            }
        }

        // Delete a vote by ID
        public async Task<bool> DeleteVoteAsync(Guid voteId)
        {
            try
            {
                if (voteId == Guid.Empty)
                {
                    _logger.LogWarning("DeleteVoteAsync: voteId is empty");
                    return false;
                }

                var voteEntity = await _context.Votes
                   .Include(v => v.ProjectIdeaDbM) 
                   .FirstOrDefaultAsync(v => v.VoteId == voteId);

                if (voteEntity == null)
                {
                    _logger.LogWarning("Vote with ID {VoteId} does not exist.", voteId);
                    return false;
                }

                // Subtract 1 from vote count
                if (voteEntity.ProjectIdeaDbM != null && voteEntity.ProjectIdeaDbM.VoteCount > 0)
                {
                    voteEntity.ProjectIdeaDbM.VoteCount -= 1;
                }

                _context.Votes.Remove(voteEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Vote deleted successfully ({VoteId})", voteId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteVoteAsync");
                throw;
            }
        }
    }
}
