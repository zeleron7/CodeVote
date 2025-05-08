using CodeVote.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVote.DbModels
{
    public class VoteDbM
    {
        [Key]
        public virtual Guid VoteId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int ProjectIdeaId { get; set; }
        public virtual IUser User { get; set; }

        [NotMapped]
        public virtual IProjectIdea ProjectIdea { get; set; }
    }
}
