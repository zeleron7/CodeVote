using AutoMapper;
using CodeVote.Data;
using CodeVote.DTO;
using CodeVote.DbModels;
using Microsoft.EntityFrameworkCore;
using CodeVote.Models;
using Microsoft.AspNetCore.Identity;

namespace CodeVote.Services
{
    public class UserService : IUserService
    {
        private readonly CodeVoteContext _context;
        private readonly IMapper _mapper;   
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher<UserDbM> _passwordHasher;

        public UserService(CodeVoteContext context, IMapper mapper, ILogger<UserService> logger, IPasswordHasher<UserDbM> passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        // Create a new user
        #region CreateUserAsync
        public async Task<ReadUserDTO> CreateUserAsync(CreateUserDTO user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("CreateUserAsync: user is null");
                    return null;
                }

                var userDbM = _mapper.Map<UserDbM>(user);

                // Hash the password
                userDbM.PasswordHash = _passwordHasher.HashPassword(userDbM, user.Password);

                await _context.Users.AddAsync(userDbM);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User created successfully with ID: {UserId}", userDbM.UserId);
                return _mapper.Map<ReadUserDTO>(userDbM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateUserAsync");
                throw;
            } 
        }
        #endregion

        // Get all users
        #region GetAllUsersAsync
        public async Task<List<ReadUserDTO>> GetAllUsersAsync()
        {
            try
            {
                var userEntities = await _context.Users
                .Include(u => u.VoteDbM)
                .ToListAsync();

                _logger.LogInformation("Retrieved {Count} users", userEntities.Count);

                return _mapper.Map<List<ReadUserDTO>>(userEntities);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error in GetAllUsersAsync");
                throw;
            }
        }
        #endregion

        // Get a user by ID
        #region GetUserByIdAsync
        public async Task<ReadUserDTO> GetUserByIdAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("GetUserByIdAsync: userId is empty");
                    return null;
                }

                var userEntity = await _context.Users
                .Include(u => u.VoteDbM)
                .FirstOrDefaultAsync(u => u.UserId == userId);

                if (userEntity == null)
                {
                    _logger.LogWarning("GetUserByIdAsync: No user found with ID: {UserId}", userId);
                    return null;
                }

                _logger.LogInformation("User retrieved successfully with ID: {UserId}", userEntity.UserId);

                return _mapper.Map<ReadUserDTO>(userEntity);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error in GetUserByIdAsync");
                throw;
            }
        }
        #endregion

        // Update a user
        #region UpdateUserAsync
        public async Task<ReadUserDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDto)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("UpdateUserAsync: userId is empty");
                    return null;
                }

                var userEntity = await _context.Users.FindAsync(userId);
                if (userEntity == null)
                {
                    _logger.LogWarning("UpdateUserAsync: userEntity is null for UserId {UserId}", userId);
                    return null;
                }

                _mapper.Map(updateUserDto, userEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User updated successfully with ID: {UserId}", userEntity.UserId);

                return _mapper.Map<ReadUserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateUserAsync");
                throw;
            }
        }
        #endregion

        // Delete a user
        #region DeleteUserAsync
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    _logger.LogWarning("DeleteUserAsync: userId is empty");
                    return false;
                }

                var userEntity = await _context.Users.FindAsync(userId);
                if (userEntity == null)
                {
                    _logger.LogWarning("DeleteUserAsync: userEntity is null for UserId {UserId}", userId);
                    return false;
                }

                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User deleted successfully with ID: {UserId}", userEntity.UserId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteUserAsync");
                throw;
            }
        }
        #endregion 
    }
}
