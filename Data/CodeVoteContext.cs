using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeVote.DbModels;

namespace CodeVote.Data
{
    public class CodeVoteContext : DbContext
    {
        public CodeVoteContext (DbContextOptions<CodeVoteContext> options)
            : base(options)
        {
        }

        public DbSet<CodeVote.DbModels.UserDbM> UserDbM { get; set; } = default!;
    }
}
