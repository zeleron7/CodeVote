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

        public VoteService(CodeVoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadVoteDTO> CreateVoteAsync(CreateVoteDTO voteDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == voteDto.UserId);
            if (!userExists)
                return null;

            var projectExists = await _context.ProjectIdeas.AnyAsync(p => p.ProjectIdeaId == voteDto.ProjectIdeaId);
            if (!projectExists)
                return null;

            // Create and save vote
            var vote = new VoteDbM
            {
                VoteId = Guid.NewGuid(),
                UserId = voteDto.UserId,         
                ProjectIdeaId = voteDto.ProjectIdeaId
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

                var savedVote = await _context.Votes
            .Include(v => v.ProjectIdeaDbM)  // Make sure to include ProjectIdea for mapping
            .FirstOrDefaultAsync(v => v.VoteId == vote.VoteId);

            // Map to ReadVoteDTO 
            return _mapper.Map<ReadVoteDTO>(vote);
        }

        public async Task<bool> DeleteVoteAsync(Guid voteId)
        {
            var voteEntity = await _context.Votes.FindAsync(voteId);
            if (voteEntity == null)
            {
                return false;
            }
            _context.Votes.Remove(voteEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
