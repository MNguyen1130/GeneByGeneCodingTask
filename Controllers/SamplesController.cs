using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeneByGeneCodingTask.Models;
using GeneByGeneCodingTask.RequestModels;
using GeneByGeneCodingTask.WebModels;

namespace GeneByGeneCodingTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Samples")]
    public class SamplesController : Controller
    {
        private readonly GeneByGeneCodingTaskDbContext _context;

        public SamplesController(GeneByGeneCodingTaskDbContext context)
        {
            _context = context;
        }

        #region Get Samples
        // GET: api/Samples
        [HttpGet]
        public IEnumerable<SamplesWebModel> GetSamples()
        {
            return _context.Samples
                .Include(s => s.Status)
                .Include(s => s.CreatedByNavigation)
                .ToList()
                .Select(s => new SamplesWebModel(s));
        }

        // GET: api/Samples/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSamples([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var samples = await _context.Samples.SingleOrDefaultAsync(m => m.SampleId == id);

            if (samples == null)
            {
                return NotFound();
            }

            return Ok(samples);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetSamples([FromBody] SamplesRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var samples = await
                _context.Samples
                .Include(s => s.Status)
                .Include(s => s.CreatedByNavigation)
                .Where(
                    s => (model.StatusId == null || s.StatusId == model.StatusId) // Search for Status only if param is not null
                    && (model.Name == null || (s.CreatedByNavigation.FirstName + " " + s.CreatedByNavigation.LastName).Contains(model.Name))
                )
                .ToListAsync();

            if (samples == null || !samples.Any())
            {
                return NotFound();
            }


            return Ok(samples.Select(s => new SamplesWebModel(s)));
        }
        #endregion

        // PUT: api/Samples/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSamples([FromRoute] int id, [FromBody] Samples samples)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != samples.SampleId)
            {
                return BadRequest();
            }

            _context.Entry(samples).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SamplesExists(id))
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

        // POST: api/Samples
        [HttpPost]
        public async Task<IActionResult> PostSamples([FromBody] Samples samples)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Samples.Add(samples);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SamplesExists(samples.SampleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSamples", new { id = samples.SampleId }, samples);
        }

        // DELETE: api/Samples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSamples([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var samples = await _context.Samples.SingleOrDefaultAsync(m => m.SampleId == id);
            if (samples == null)
            {
                return NotFound();
            }

            _context.Samples.Remove(samples);
            await _context.SaveChangesAsync();

            return Ok(samples);
        }

        private bool SamplesExists(int id)
        {
            return _context.Samples.Any(e => e.SampleId == id);
        }

        private bool IsValid(Samples sample)
        {
            return sample.Barcode != null
                && sample.CreatedAt != null
                && sample.CreatedBy != null
                && sample.StatusId != null;
        }
    }
}