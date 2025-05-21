using CodeVote.DbModels;

namespace CodeVote.Models.DTO
{
    public class ReadUserDTO
    {
        public Guid UserId { get; set; }           
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public List<ReadVoteDTO> Votes { get; set; } = new List<ReadVoteDTO>();
    }
}
