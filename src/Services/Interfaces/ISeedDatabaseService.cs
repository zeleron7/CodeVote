using CodeVote.Data;

namespace CodeVote.src.Services.Interfaces
{
    public interface ISeedDatabaseService
    {
        public Task SeedDatabaseAsync(CodeVoteContext context, Guid? userId);
        public Task ClearDatabaseAsync(CodeVoteContext context);
    }
}
