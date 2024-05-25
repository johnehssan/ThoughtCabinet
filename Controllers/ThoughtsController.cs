using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThoughtCabinet.Data;
using ThoughtCabinet.Models;

namespace ThoughtCabinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Thought>> GetThoughtById(int id)
        {
            var thought = await _context.Thoughts.FindAsync(id);
            if (thought == null)
            {
                return NotFound();
            }

            return thought;
        }

        [HttpPost]
        public async Task<ActionResult<Thought>> CreateThought(Thought thought)
        {
            _context.Thoughts.Add(thought);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetThoughtById), new { id = thought.Id }, thought);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThought(int id, Thought thought)
        {
            if (id != thought.Id)
            {
                return BadRequest();
            }

            _context.Entry(thought).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
    }
}
