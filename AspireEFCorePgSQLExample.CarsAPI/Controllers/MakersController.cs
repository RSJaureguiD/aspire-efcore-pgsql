using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspireEFCorePgSQLExample.CarsAPI.DAL;
using AspireEFCorePgSQLExample.CarsAPI.DAL.Models;

namespace AspireEFCorePgSQLExample.CarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakersController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public MakersController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Makers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maker>>> GetMakers()
        {
            return await _context.Makers.ToListAsync();
        }

        // GET: api/Makers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maker>> GetMaker(Guid id)
        {
            var maker = await _context.Makers.FindAsync(id);

            if (maker == null)
            {
                return NotFound();
            }

            return maker;
        }

        // PUT: api/Makers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaker(Guid id, Maker maker)
        {
            if (id != maker.Guid)
            {
                return BadRequest();
            }

            _context.Entry(maker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakerExists(id))
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

        // POST: api/Makers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maker>> PostMaker(Maker maker)
        {
            _context.Makers.Add(maker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaker", new { id = maker.Guid }, maker);
        }

        // DELETE: api/Makers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaker(Guid id)
        {
            var maker = await _context.Makers.FindAsync(id);
            if (maker == null)
            {
                return NotFound();
            }

            _context.Makers.Remove(maker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MakerExists(Guid id)
        {
            return _context.Makers.Any(e => e.Guid == id);
        }
    }
}
