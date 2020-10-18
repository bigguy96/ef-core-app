using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samurai.Data;

namespace SamuraiWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly SamuraiContext _context;

        public SamuraisController(SamuraiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of samurais.
        /// </summary>
        /// <returns>List os samurais</returns>
        /// <response code="200">Returns a list of samurais</response>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Samurai.Domain.Samurai>))]
        public async Task<ActionResult<IEnumerable<Samurai.Domain.Samurai>>> GetSamurais()
        {
            return await _context.Samurais.Select(samurai => new Samurai.Domain.Samurai
            {
                Id = samurai.Id,
                Name = samurai.Name,
                Clan = samurai.Clan,
                Horse = samurai.Horse,
                Quotes = samurai.Quotes,
                SamuraiBattles = samurai.SamuraiBattles
            }).ToListAsync();            
        }

        /// <summary>
        /// Gets a samurai.
        /// </summary>
        /// <param name="id">Samurai's id</param>
        /// <returns>A samurai, otherwise not found.</returns>
        /// <response code="200">Returns a samurai</response>
        /// <response code="404">Samurai doesn't exist</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Samurai.Domain.Samurai))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Samurai.Domain.Samurai>> GetSamurai(int id)
        {
            var samurai = await _context.Samurais.FindAsync(id);

            if (samurai == null) return NotFound();

            return samurai;
        }

        /// <summary>
        /// Updates a samurai.
        /// </summary>
        /// <param name="id">Samurai's id</param>
        /// <param name="samurai">Samurai details</param>
        /// <returns>No content if successful, otherwise 400.</returns>
        /// <response code="204">Saumrai updated successfully</response>
        /// <response code="400">Samurai doesn't exist</response> 
        /// <response code="404">Samurai not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSamurai(int id, Samurai.Domain.Samurai samurai)
        {
            if (id != samurai.Id)
            {
                return BadRequest();
            }

            _context.Entry(samurai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SamuraiExists(id))
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

        /// <summary>
        /// Adds a new samurai.
        /// </summary>
        /// <param name="samurai">Samurai details</param>
        /// <returns>Returns 201 with link to new samurai.</returns>
        /// <response code="201">Newly created samurai</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<Samurai.Domain.Samurai>> PostSamurai(Samurai.Domain.Samurai samurai)
        {
            _context.Samurais.Add(samurai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSamurai", new { id = samurai.Id }, samurai);
        }

        /// <summary>
        /// Deletes a samurai.
        /// </summary>
        /// <param name="id">Samurai's id</param>
        /// <returns>Samurai details, otherwise not found.</returns>
        /// <response code="200">Deleted samurai</response>
        /// <response code="404">Samurai not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Samurai.Domain.Samurai))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Samurai.Domain.Samurai>> DeleteSamurai(int id)
        {
            var samurai = await _context.Samurais.FindAsync(id);
            if (samurai == null)
            {
                return NotFound();
            }

            _context.Samurais.Remove(samurai);
            await _context.SaveChangesAsync();

            return samurai;
        }

        private bool SamuraiExists(int id)
        {
            return _context.Samurais.Any(e => e.Id == id);
        }
    }
}