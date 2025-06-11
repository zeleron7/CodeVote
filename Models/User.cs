using CodeVote.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CodeVote.Models
{
    public class User : IUser 
    {
        public virtual Guid UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PasswordHash { get; set; }

        // User can have multiple votes
        public virtual List<IVote> Votes { get; set; } = null;
    }
}
