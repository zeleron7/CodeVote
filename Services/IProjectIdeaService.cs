using CodeVote.Models.DTO;

namespace CodeVote.Services
{
    public interface IProjectIdeaService
    {
        Task<ReadProjectIdeaDTO> CreateProjectIdeaAsync(CreateProjectIdeaDTO projectidea);
        Task<List<ReadProjectIdeaDTO>> GetAllProjectIdeasAsync();
        Task<ReadProjectIdeaDTO> GetProjectIdeaByIdAsync(Guid projectideaId);
        Task<ReadProjectIdeaDTO> UpdateProjectIdeaAsync(Guid projectideaId, UpdateProjectIdeaDTO updateprojectideaDto);
        Task<bool> DeleteProjectIdeaAsync(Guid projectideaId);
    }
}
