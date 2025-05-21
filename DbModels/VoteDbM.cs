using CodeVote.Interfaces;
using CodeVote.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeVote.DbModels
{
    public class VoteDbM : Vote
    {
        [Key]
        public override Guid VoteId { get; set; }

        public override Guid UserId { get; set; }
        public override Guid ProjectIdeaId { get; set; }




        [NotMapped]
        public override IUser User { get => UserDbM; set => throw new NotImplementedException(); }

        [NotMapped]
        public override IProjectIdea ProjectIdea { get => ProjectIdeaDbM; set => throw new NotImplementedException(); }

        [ForeignKey(nameof(ProjectIdeaId))]
        [JsonIgnore]
        public virtual ProjectIdeaDbM ProjectIdeaDbM { get; set; } = null;

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual UserDbM UserDbM { get; set; } = null;
    }
}
