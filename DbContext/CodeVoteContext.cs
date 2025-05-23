using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeVote.DbModels;
using Microsoft.EntityFrameworkCore.Design;

namespace CodeVote.Data
{
    public class CodeVoteContext : DbContext
    {
        public CodeVoteContext (DbContextOptions<CodeVoteContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectIdeaDbM> ProjectIdeas { get; set; }
        public DbSet<UserDbM> Users { get; set; }
        public DbSet<VoteDbM> Votes { get; set; }
    }
}
