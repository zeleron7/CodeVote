namespace CodeVote.Models.DTO
{
    public class CreateVoteDTO
    {
        public Guid UserId { get; set; }
        public Guid ProjectIdeaId { get; set; }
    }
}
