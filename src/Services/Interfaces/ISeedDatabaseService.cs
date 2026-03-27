using CodeVote.Data;

namespace CodeVote.src.Services.Interfaces
{
    public interface ISeedDatabaseService
    {
        Task SeedDatabaseAsync(CodeVoteContext context, Guid? userId);
        Task ClearDatabaseAsync(CodeVoteContext context);
    }
}
