using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeVote.src.DbModels
{
    public class UserDbM 
    {
        // PK
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        // User can have multiple project ideas
        public List<ProjectIdeaDbM> ProjectIdeas { get; set; }

        // User can have multiple votes
        public List<VoteDbM> Votes { get; set; }
    }
}
