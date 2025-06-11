using CodeVote.Data;

namespace CodeVote.src.Services.Interfaces
{
    public interface ISeedDatabaseService
    {
        public Task SeedDatabaseAsync(CodeVoteContext context);
        public Task ClearDatabaseAsync(CodeVoteContext context);
    }
}
