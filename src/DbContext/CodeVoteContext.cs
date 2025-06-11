using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CodeVote.src.DbModels;

namespace CodeVote.Data
{
    public class CodeVoteContext : DbContext
    {
        public CodeVoteContext(DbContextOptions<CodeVoteContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectIdeaDbM> ProjectIdeas { get; set; }
        public DbSet<UserDbM> Users { get; set; }
        public DbSet<VoteDbM> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fix cascade issue
            modelBuilder.Entity<VoteDbM>()
                .HasOne(v => v.ProjectIdea)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.ProjectIdeaId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<VoteDbM>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
    
}
