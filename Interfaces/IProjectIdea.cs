namespace CodeVote.Interfaces
{
    public interface IProjectIdea
    {
        public Guid ProjectIdeaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int VoteCount { get; set; }

        // ProjectIdea can have multiple votes
        public List<IVote> Votes { get; set; }
    }
}
