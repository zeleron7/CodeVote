using CodeVote.Interfaces;
using CodeVote.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeVote.DbModels
{
    public class ProjectIdeaDbM : ProjectIdea
    {
        [Key]
        public override Guid ProjectIdeaId { get; set; }
        public override string Title { get; set; }
        public override string Description { get; set; }
        public override int VoteCount { get; set; }

        [NotMapped]
        public override List<IVote> Votes { get => VoteDbM?.ToList<IVote>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public virtual List<VoteDbM> VoteDbM { get; set; } = null;
    }
}
