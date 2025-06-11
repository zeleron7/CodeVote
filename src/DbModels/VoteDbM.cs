using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeVote.src.DbModels
{
    public class VoteDbM 
    {
        // PK
        [Key]
        public Guid VoteId { get; set; }
        // FK
        public Guid UserId { get; set; }
        public Guid ProjectIdeaId { get; set; }

        // Navigation properties
        public UserDbM User { get; set; }
        public ProjectIdeaDbM ProjectIdea { get; set; }
    }
}
