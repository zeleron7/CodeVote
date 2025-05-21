using AutoMapper;
using CodeVote.Data;
using CodeVote.Models.DTO;
using CodeVote.DbModels;
using Microsoft.EntityFrameworkCore;
using CodeVote.Models;

namespace CodeVote.Services
{
    public class ProjectIdeaService : IProjectIdeaService
    {
        private readonly CodeVoteContext _context;
        private readonly IMapper _mapper;

        public ProjectIdeaService(CodeVoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadProjectIdeaDTO> CreateProjectIdeaAsync(CreateProjectIdeaDTO projectIdea)
        {
            var projectIdeaDbM = _mapper.Map<ProjectIdeaDbM>(projectIdea);
            await _context.ProjectIdeas.AddAsync(projectIdeaDbM);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadProjectIdeaDTO>(projectIdeaDbM);
        }

        public async Task<List<ReadProjectIdeaDTO>> GetAllProjectIdeasAsync()
        {
            var projectIdeaEntities = await _context.ProjectIdeas
                .Include(p => p.VoteDbM)
                .ToListAsync();
            return _mapper.Map<List<ReadProjectIdeaDTO>>(projectIdeaEntities);
        }
    }
}
