using AutoMapper;
using CodeVote.Data;
using CodeVote.Models.DTO;
using CodeVote.DbModels;
using Microsoft.EntityFrameworkCore;
using CodeVote.Models;

namespace CodeVote.Services
{
    public class UserService : IUserService
    {
        private readonly CodeVoteContext _context;
        private readonly IMapper _mapper;   

        public UserService(CodeVoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadUserDTO> CreateUserAsync(CreateUserDTO user)
        {
            var userDbM = _mapper.Map<UserDbM>(user);
            await _context.Users.AddAsync(userDbM);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadUserDTO>(userDbM);
        }

        public async Task<List<ReadUserDTO>> GetAllUsersAsync()
        {
            var userEntities = await _context.Users
                .Include(u => u.VoteDbM)
                .ToListAsync();

            return _mapper.Map<List<ReadUserDTO>>(userEntities);
        }

        public async Task<ReadUserDTO> GetUserByIdAsync(Guid userId)
        {
            var userEntity = await _context.Users
                .Include(u => u.VoteDbM)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (userEntity == null)
            {
                return null;
            }
            return _mapper.Map<ReadUserDTO>(userEntity);
        }

        public async Task<ReadUserDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDto)
        {
            var userEntity = await _context.Users.FindAsync(userId);
            if (userEntity == null)
            {
                return null;
            }
            _mapper.Map(updateUserDto, userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadUserDTO>(userEntity);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var userEntity = await _context.Users.FindAsync(userId);
            if (userEntity == null)
            {
                return false;
            }
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
