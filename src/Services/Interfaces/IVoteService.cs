using CodeVote.src.DTO;

namespace CodeVote.src.Services.Interfaces
{
    public interface IVoteService
    {
        Task<ReadVoteDTO> CreateVoteAsync(CreateVoteDTO vote);
        Task<bool> DeleteVoteAsync(Guid voteId);
    }
}
