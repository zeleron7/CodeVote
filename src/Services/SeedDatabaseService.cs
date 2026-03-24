using CodeVote.Data;   
using CodeVote.src.DbModels;
using CodeVote.src.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeVote.src.Services
{
    public class SeedDatabaseService : ISeedDatabaseService
    {
        const string seedSource = "./projectIdeas.json";
        private readonly ILogger<SeedDatabaseService> _logger;

        public SeedDatabaseService(ILogger<SeedDatabaseService> logger)
        {
            _logger = logger;   
        }

        // SeedDatabaseAsync
        #region SeedDatabaseAsync
        public async Task SeedDatabaseAsync(CodeVoteContext context, Guid? userId)
        {
            try
            {
                string jsonData = await File.ReadAllTextAsync(seedSource);
                if (string.IsNullOrWhiteSpace(jsonData))
                {
                    _logger.LogError("Seed data file is empty or not found at {SeedSource}", seedSource);
                    throw new InvalidOperationException();
                }

                var projectIdeas = System.Text.Json.JsonSerializer.Deserialize<List<ProjectIdeaDbM>>(jsonData);

                // Assign new project ID and user ID to each project idea before saving to the database
                projectIdeas = projectIdeas.Select(pi =>
                {
                    pi.ProjectIdeaId = Guid.NewGuid();
                    pi.UserId = (Guid)userId;
                    return pi;
                }).ToList();

                await context.AddRangeAsync(projectIdeas);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding database from file {SeedSource}", seedSource);
                throw;
            }
        }
        #endregion 

        // ClearDatabaseAsync
        #region ClearDatabaseAsync
        public async Task ClearDatabaseAsync(CodeVoteContext context)
        {
            try
            {
                // Remove votes first to avoid foreign key constraint issues when removing project ideas
                if (context.Votes.Any())
                {
                    context.Votes.RemoveRange(context.Votes);
                    await context.SaveChangesAsync();
                }

                if (context.ProjectIdeas.Any())
                {
                    context.ProjectIdeas.RemoveRange(context.ProjectIdeas);
                    context.SaveChanges();
                }
                _logger.LogInformation("No project ideas found to clear.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing database");
                throw;
            }
        }
        #endregion 
    }
}
