using CodeVote.DTO;

namespace CodeVote.Services
{
    public interface IVoteService
    {
        Task<ReadVoteDTO> CreateVoteAsync(CreateVoteDTO vote);
        Task<bool> DeleteVoteAsync(Guid voteId);
    }
}
