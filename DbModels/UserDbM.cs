using CodeVote.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeVote.DbModels
{
    public class UserDbM
    {
        [Key]
        public virtual Guid UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PasswordHash { get; set; }

        [NotMapped]
        public virtual List<IVote> Votes { get; set; }
    }
}
