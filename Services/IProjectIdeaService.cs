using CodeVote.Models.DTO;

namespace CodeVote.Services
{
    public interface IProjectIdeaService
    {
        Task<ReadProjectIdeaDTO> CreateProjectIdeaAsync(CreateProjectIdeaDTO projectidea);
        Task<List<ReadProjectIdeaDTO>> GetAllProjectIdeasAsync();
    }
}
