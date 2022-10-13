using Microsoft.AspNetCore.Mvc;
using MicroserviceSportsMan.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSportsMan.Controllers
{
    [ApiController]
    [Route("api/sportsmen")]
    public class SportsManController : ControllerBase
    {
        private readonly SportsManContext? _context;
        public SportsManController(SportsManContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("SportsMan")]
        public async Task<ActionResult<SportsMan>> GetSportsMan(int id)
        {
            var sportsMan = await _context!.SportsMen.FindAsync(id);

            if (sportsMan == null)
            {
                return NotFound();
            }

            return sportsMan;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutSportsMan(int id, SportsMan sportsMan)
        {
            if (id != sportsMan.Id)
            {
                return BadRequest();
            }

            _context!.Entry(sportsMan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportsManExists(id))
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSportsMan(int id)
        {
            var sportsMan = await _context!.SportsMen.FindAsync(id);
            if (sportsMan == null)
            {
                return NotFound();
            }

            _context.SportsMen.Remove(sportsMan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult<SportsMan>> CreateSportsMan(SportsMan sportsMan)
        {
            _context!.SportsMen.Add(sportsMan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetSportsMan),
                new { id = sportsMan.Id }, sportsMan);
        }
        private bool SportsManExists(int id)
        {
            return _context!.SportsMen.Any(x => x.Id == id);
        }

    }
}
