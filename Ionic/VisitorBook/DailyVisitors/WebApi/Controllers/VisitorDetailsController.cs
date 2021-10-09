using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyVisitors.DAL.Models;
using System.IO;

namespace DailyVisitors.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorDetailsController : ControllerBase
    {
        private readonly VisitorsBookContext _context;

        public VisitorDetailsController(VisitorsBookContext context)
        {
            _context = context;
        }

        // GET: api/VisitorDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDetails>>> GetVisitorDetails()
        {
            return await _context.VisitorDetails.ToListAsync();
        }

		//GET: api/VisitorDetail/5
        [HttpGet("{id}")]
		public async Task<ActionResult<VisitorDetails>> GetVisitorDetails(long id)
		{
			var visitorDetails = await _context.VisitorDetails.FindAsync(id);

			if (visitorDetails == null)
			{
				return NotFound();
			}

			return visitorDetails;
		}

		// PUT: api/VisitorDetail/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutVisitorDetails(long id, VisitorDetails visitorDetails)
        {
            if (id != visitorDetails.VisitorId)
            {
                return  BadRequest();
            }

            _context.Entry(visitorDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorDetailsExists(id))
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

        // POST: api/VisitorDetail
        [HttpPost]
        public async Task<ActionResult<VisitorDetails>> PostVisitorDetails(VisitorDetails visitorDetails)
        {
            try
            {
                visitorDetails.Signature = SaveImage(visitorDetails.Signature);
                visitorDetails.Picture = SaveImage(visitorDetails.Picture);
                visitorDetails.LoginDateTime = DateTime.UtcNow;
                visitorDetails.LogoutDateTime = null;
                visitorDetails.IsDeleted = false;
                _context.VisitorDetails.Add(visitorDetails);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetVisitorDetails", new { id = visitorDetails.VisitorId }, visitorDetails);
            }
            catch(Exception ex)
            {
                return CreatedAtAction("Error", ex.Message.ToString());
            }
           
        }

        // DELETE: api/VisitorDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<VisitorDetails>>> DeleteVisitorDetails(long id)
        {
            var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
            if (id != visitorDetails.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitorDetails).State = EntityState.Modified;
            visitorDetails.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await _context.VisitorDetails.Where(x=>x.IsDeleted==false).ToListAsync();
        }

        //api/VisitorDetails/logoutById?id=1
        [HttpGet("logoutById")]
        public async Task<ActionResult<IEnumerable<VisitorDetails>>> Get(int id)
        {
            var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
            if (id != visitorDetails.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitorDetails).State = EntityState.Modified;
            visitorDetails.LogoutDateTime = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await _context.VisitorDetails.Where(x=>x.IsDeleted==false).ToListAsync();
        }

        private bool VisitorDetailsExists(long id)
        {
            return _context.VisitorDetails.Any(e => e.VisitorId == id);
        }

        public string SaveImage(string base64image)
        {
            var bytes = Convert.FromBase64String(base64image);      
            var folderName = Path.Combine("StaticFiles", "Images");
            var filedir = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(filedir))
            {
                Directory.CreateDirectory(filedir);
            }
            var timestamp = DateTime.Now.ToFileTime();
            var fileName = timestamp.ToString() + ".png";
            var file = Path.Combine(filedir, fileName);
            if (bytes.Length > 0)
            {
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
            return Path.Combine(folderName, fileName);
        }
    }
}
