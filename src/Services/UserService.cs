using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CodeVote.Data;
using CodeVote.src.DbModels;
using CodeVote.src.DTO;
using CodeVote.src.Services.Interfaces;

namespace CodeVote.src.Services
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

                // Check if user with the same username already exists
                var userExists = await _context.Users
                    .AnyAsync(u => u.UserName == user.UserName);

                if (userExists)
                {
                    _logger.LogWarning("CreateUserAsync: User with username {UserName} already exists", user.UserName);
                    return null; // handling null in AutoMapper > MappingProfile.cs
                }

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
                .Include(u => u.Votes)
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
        public async Task<ReadUserDTO> GetUserByIdAsync(Guid user, Guid? userId)
        {
            try
            {
                if (user == Guid.Empty)
                {
                    _logger.LogWarning("GetUserByIdAsync: userId is empty");
                    return null;
                }

                var userEntity = await _context.Users
                .Include(u => u.Votes)
                .FirstOrDefaultAsync(u => u.UserId == user);

                if (userEntity == null)
                {
                    _logger.LogWarning("GetUserByIdAsync: No user found with ID: {UserId}", user);
                    return null;
                }
                // Check if the user is authorized to update the project idea (i.e., they are the owner of the project idea)
                if (userEntity.UserId != userId)
                {
                    _logger.LogWarning("GetUserByIdAsync: User {UserId} is not authorized to retrieve user information with ID {user}", userId, user);
                    return null; // Or throw an exception if you prefer
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
        public async Task<bool> DeleteUserAsync(Guid user, Guid? userId)
        {
            try
            {
                if (user == Guid.Empty)
                {
                    _logger.LogWarning("DeleteUserAsync: userId is empty");
                    return false;
                }

                var userEntity = await _context.Users.FindAsync(user);
                if (userEntity == null)
                {
                    _logger.LogWarning("DeleteUserAsync: userEntity is null for UserId {UserId}", user);
                    return false;
                }
                // Check if the user is authorized to delete the user (i.e., they are the user)
                if (userEntity.UserId != userId)
                {
                    _logger.LogWarning("DeleteUserAsync: User {UserId} is not authorized to delete user with ID {User}", userId, user);
                    return false; // Or throw an exception if you prefer
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
