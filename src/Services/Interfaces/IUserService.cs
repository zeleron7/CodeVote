using CodeVote.src.DTO;

namespace CodeVote.src.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDTO> CreateUserAsync(CreateUserDTO user);
        Task<List<ReadUserDTO>> GetAllUsersAsync();
        Task<ReadUserDTO> GetUserByIdAsync(Guid user, Guid? userId);
        Task<ReadUserDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDto);
        Task<bool> DeleteUserAsync(Guid user, Guid? userId);
    }
}
