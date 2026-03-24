using CodeVote.src.DTO;

namespace CodeVote.src.Services.Interfaces
{
    public interface IProjectIdeaService
    {
        Task<ReadProjectIdeaDTO> CreateProjectIdeaAsync(CreateProjectIdeaDTO projectidea, Guid? userId);
        Task<List<ReadProjectIdeaDTO>> GetAllProjectIdeasAsync();
        Task<ReadProjectIdeaDTO> GetProjectIdeaByIdAsync(Guid projectideaId);
        Task<ReadProjectIdeaDTO> UpdateProjectIdeaAsync(Guid projectideaId, UpdateProjectIdeaDTO updateprojectideaDto, Guid? userId);
        Task<bool> DeleteProjectIdeaAsync(Guid projectideaId, Guid? userId);
    }
}
