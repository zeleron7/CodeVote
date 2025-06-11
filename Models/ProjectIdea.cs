using CodeVote.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CodeVote.Models
{
    public class ProjectIdea : IProjectIdea
    {
        public virtual Guid ProjectIdeaId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual int VoteCount { get; set; }

        // ProjectIdea can have multiple votes
        public virtual List<IVote> Votes { get; set; } = null;
    }
}
