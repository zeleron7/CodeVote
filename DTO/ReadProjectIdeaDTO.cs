namespace CodeVote.DTO
{
    public class ReadProjectIdeaDTO
    {
        public Guid ProjectIdeaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int VoteCount { get; set; }
    }
}
