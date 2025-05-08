using CodeVote.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVote.DbModels
{
    public class ProjectIdeaDbM
    {
        [Key]
        public virtual Guid ProjectIdeaId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int VoteCount { get; set; }

        [NotMapped]
        public virtual List<IVote> Votes { get; set; }
    }
}
