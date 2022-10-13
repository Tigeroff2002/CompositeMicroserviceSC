using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroserviceSportsMan.Models;
using System.Net.Http;
using System.Text.Json;

namespace MicroserviceSportsMan
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsMenController : ControllerBase
    {
        private readonly SportsManContext _context;

        public SportsMenController(SportsManContext context)
        {
            _context = context;
        }
        /*
        [HttpGet("organization/{sportsmanId}")]
        public async Task<string>? GetOrganization(int sportsmanId)
        {
            var actionResult = await GetSportsMan(sportsmanId);
            var sportsman = actionResult.Value;
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (
                sender, cert, chain, sslPolicyErrors) =>
                 { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await
                    client.GetAsync($"https://localhost:7163/api/organizations/" +
                        $"{sportsman!.OrganizationId}");
                if (response.IsSuccessStatusCode)
                {
                    object? organization = await JsonSerializer.DeserializeAsync<object>(
                        await response!.Content.ReadAsStreamAsync());
                    return organization!.ToString()!;
                }
            }
            return null!;
        }
        */

        // GET: api/SportsMen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportsMan>>> GetSportsMen()
        {
            return await _context.SportsMen.ToListAsync();
        }

        // GET: api/SportsMen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SportsMan>> GetSportsMan(int id)
        {
            var sportsMan = await _context.SportsMen.FindAsync(id);

            if (sportsMan == null)
            {
                return NotFound();
            }

            return sportsMan;
        }

        // PUT: api/SportsMen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSportsMan(int id, SportsMan sportsMan)
        {
            if (id != sportsMan.Id)
            {
                return BadRequest();
            }

            _context.Entry(sportsMan).State = EntityState.Modified;

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

        // POST: api/SportsMen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SportsMan>> PostSportsMan(SportsMan sportsMan)
        {
            _context.SportsMen.Add(sportsMan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSportsMan", new { id = sportsMan.Id }, sportsMan);
        }

        // DELETE: api/SportsMen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSportsMan(int id)
        {
            var sportsMan = await _context.SportsMen.FindAsync(id);
            if (sportsMan == null)
            {
                return NotFound();
            }

            _context.SportsMen.Remove(sportsMan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SportsManExists(int id)
        {
            return _context.SportsMen.Any(e => e.Id == id);
        }
    }
}
