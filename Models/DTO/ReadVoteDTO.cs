namespace CodeVote.Models.DTO
{
    public class ReadVoteDTO
    {
        public Guid VoteId { get; set; }
        public int UserId { get; set; }
        public int ProjectIdeaId { get; set; }
    }
}
