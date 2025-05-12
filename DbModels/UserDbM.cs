using CodeVote.Interfaces;
using CodeVote.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeVote.DbModels
{
    public class UserDbM : User
    {
        [Key]
        public override Guid UserId { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override string Email { get; set; }
        public override string UserName { get; set; }
        public override string PasswordHash { get; set; }

        [NotMapped]
        public override List<IVote> Votes { get => VoteDbM?.ToList<IVote>(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public virtual List<VoteDbM> VoteDbM { get; set; } = null;
    }
}
