using CodeVote.Data;

namespace CodeVote.Services
{
    public interface ISeedDatabaseService
    {
        public Task SeedDatabaseAsync(CodeVoteContext context);
        public Task ClearDatabaseAsync(CodeVoteContext context);
    }
}
