using CodeVote.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CodeVote.Models
{
    public class Vote : IVote
    {
        public virtual Guid VoteId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual Guid ProjectIdeaId { get; set; }

        // Foreign keys
        public virtual IUser User { get; set; }
        public virtual IProjectIdea ProjectIdea { get; set; }
    }
}
