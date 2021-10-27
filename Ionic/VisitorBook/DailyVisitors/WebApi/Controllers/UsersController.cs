using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyVisitors.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace DailyVisitors.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VisitorsBookContext _context;

        public UsersController(VisitorsBookContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            //return await _context.Users.ToListAsync();
            return await _context.Users.ToListAsync();
        }

        //[Authorize]
        // GET: api/Users/GetRWSUsers
        [HttpGet("GetRWSUsers")]
        public async Task<ActionResult<IEnumerable<Users>>> GetRWSUsers()
        {
            //return await _context.Users.ToListAsync();
            return await _context.Users.Where(x => (x.Active == true) && (x.EmpowerUserId != 0)).ToListAsync();
        }

        //[Authorize]
        // GET: api/Users/GetUsersDisplayName
        [HttpGet("GetUsersDisplayName")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsersDisplayName()
        {
            return await _context.Users
                .Where(x =>(x.EmpowerUserId == 0))
                .Select(x=>x.DisplayName.ToString().Trim().Replace("\n","").Replace("\r", ""))
                .ToListAsync();
        }

        //[Authorize]
        [HttpGet("GetOffice")]
        // GET: api/Users/GetOffice
        public async Task<ActionResult<IEnumerable<Office>>> GetOffice()
        {
            return await _context.Office
                .Where(x => x.Active == true)
                .ToListAsync();
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> Post(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
