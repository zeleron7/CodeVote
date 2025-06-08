using CodeVote.Data;
using CodeVote.DbModels;
using CodeVote.DTO;
using Microsoft.EntityFrameworkCore;

namespace CodeVote.Services
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
        public async Task SeedDatabaseAsync(CodeVoteContext context)
        {
            string jsonData = await File.ReadAllTextAsync(seedSource);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                _logger.LogError("Seed data file is empty or not found at {SeedSource}", seedSource);
                throw new InvalidOperationException();
            }

            var projectIdeas = System.Text.Json.JsonSerializer.Deserialize<List<ProjectIdeaDbM>>(jsonData);

            await context.AddRangeAsync(projectIdeas);
            await context.SaveChangesAsync();
        }
        #endregion 

        // ClearDatabaseAsync
        #region ClearDatabaseAsync
        public async Task ClearDatabaseAsync(CodeVoteContext context)
        {
            var projectIdeas = context.ProjectIdeas;

            if (projectIdeas == null || !projectIdeas.Any())
            {
                _logger.LogInformation("No project ideas found to clear.");
                return;
            }

            context.RemoveRange(projectIdeas);
            context.SaveChanges();
        }
        #endregion 
    }
}
