namespace CodeVote.Interfaces
{
    public interface IUser
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public List<IVote> Votes { get; set; }
    }
}
