using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThoughtCabinet.Data;
using ThoughtCabinet.Models;

namespace ThoughtCabinet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThoughtsController : ControllerBase
    {
        private readonly ThoughtsContext _context;

        public ThoughtsController(ThoughtsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Thought>>> GetThoughts()
        {
            return await _context.Thoughts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Thought>> GetThought(int id)
        {
            var thought = await _context.Thoughts.FindAsync(id);

            if (thought == null)
            {
                return NotFound();
            }

            return thought;
        }

        [HttpPost]
        public async Task<ActionResult<Thought>> PostThought(Thought thought)
        {
            thought.CreatedAt = DateTime.UtcNow;
            thought.UpdatedAt = DateTime.UtcNow;
            _context.Thoughts.Add(thought);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetThought), new { id = thought.Id }, thought);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutThought(int id, Thought thought)
        {
            if (id != thought.Id)
            {
                return BadRequest();
            }

            thought.UpdatedAt = DateTime.UtcNow;
            _context.Entry(thought).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThoughtExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThought(int id)
        {
            var thought = await _context.Thoughts.FindAsync(id);
            if (thought == null)
            {
                return NotFound();
            }

            _context.Thoughts.Remove(thought);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThoughtExists(int id)
        {
            return _context.Thoughts.Any(e => e.Id == id);
        }
    }
}
