namespace CodeVote.Interfaces
{
    public interface IVote
    {
        public Guid VoteId { get; set; }
        public int UserId { get; set; }
        public int ProjectIdeaId { get; set; }

        public IUser User { get; set; }
        public IProjectIdea ProjectIdea { get; set; }
    }
}
