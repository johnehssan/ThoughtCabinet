using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<Thoughts>>> GetNotes()
        {
            return await _context.Thoughts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Thoughts>> GetNoteById(int id)
        {
            var note = await _context.Thoughts.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPost]
        public async Task<ActionResult<Thoughts>> CreateNote(Thoughts note)
        {
            _context.Thoughts.Add(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, Thoughts note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Thoughts.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Thoughts.Remove(note);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
