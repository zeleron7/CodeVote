using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeVote.Data;
using CodeVote.DbModels;

namespace CodeVote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CodeVoteContext _context;

        public UserController(CodeVoteContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDbM>>> GetUserDbM()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDbM>> GetUserDbM(Guid id)
        {
            var userDbM = await _context.Users.FindAsync(id);

            if (userDbM == null)
            {
                return NotFound();
            }

            return userDbM;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDbM(Guid id, UserDbM userDbM)
        {
            if (id != userDbM.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userDbM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDbMExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDbM>> PostUserDbM(UserDbM userDbM)
        {
            _context.Users.Add(userDbM);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDbM", new { id = userDbM.UserId }, userDbM);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDbM(Guid id)
        {
            var userDbM = await _context.Users.FindAsync(id);
            if (userDbM == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userDbM);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDbMExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
