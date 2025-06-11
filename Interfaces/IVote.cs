namespace CodeVote.Interfaces
{
    public interface IVote
    {
        public Guid VoteId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectIdeaId { get; set; }

        // Foreign keys
        public IUser User { get; set; }
        public IProjectIdea ProjectIdea { get; set; }
    }
}
