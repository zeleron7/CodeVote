using CodeVote.DTO;

namespace CodeVote.Services
{
    public interface IUserService
    {
        Task<ReadUserDTO> CreateUserAsync(CreateUserDTO user);
        Task<List<ReadUserDTO>> GetAllUsersAsync();
        Task<ReadUserDTO> GetUserByIdAsync(Guid userId);
        Task<ReadUserDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDto);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
