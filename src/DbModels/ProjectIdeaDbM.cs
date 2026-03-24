using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeVote.src.DbModels
{
    public class ProjectIdeaDbM 
    {
        // PK
        [Key]
        public Guid ProjectIdeaId { get; set; }
        // FK
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int VoteCount { get; set; }

        // navigation property
        public UserDbM User { get; set; }

        // ProjectIdea can have multiple votes
        public List<VoteDbM> Votes { get; set; }
    }
}
