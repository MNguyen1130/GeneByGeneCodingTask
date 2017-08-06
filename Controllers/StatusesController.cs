using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneByGeneCodingTask.Models;

namespace GeneByGeneCodingTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Statuses")]
    public class StatusesController : Controller
    {
        private readonly GeneByGeneCodingTaskDbContext _context;

        public StatusesController(GeneByGeneCodingTaskDbContext context)
        {
            _context = context;
        }

        // GET: api/Statuses
        [HttpGet]
        public IEnumerable<Statuses> GetStatuses()
        {
            return _context.Statuses;
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatuses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var statuses = await _context.Statuses.SingleOrDefaultAsync(m => m.StatusId == id);

            if (statuses == null)
            {
                return NotFound();
            }

            return Ok(statuses);
        }

        // PUT: api/Statuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatuses([FromRoute] int id, [FromBody] Statuses statuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statuses.StatusId)
            {
                return BadRequest();
            }

            _context.Entry(statuses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusesExists(id))
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

        // POST: api/Statuses
        [HttpPost]
        public async Task<IActionResult> PostStatuses([FromBody] Statuses statuses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Statuses.Add(statuses);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StatusesExists(statuses.StatusId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStatuses", new { id = statuses.StatusId }, statuses);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatuses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var statuses = await _context.Statuses.SingleOrDefaultAsync(m => m.StatusId == id);
            if (statuses == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(statuses);
            await _context.SaveChangesAsync();

            return Ok(statuses);
        }

        private bool StatusesExists(int id)
        {
            return _context.Statuses.Any(e => e.StatusId == id);
        }
    }
}