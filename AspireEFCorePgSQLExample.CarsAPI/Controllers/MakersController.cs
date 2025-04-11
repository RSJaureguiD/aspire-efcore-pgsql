using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspireEFCorePgSQLExample.CarsAPI.DAL;
using AspireEFCorePgSQLExample.CarsAPI.DAL.Models;
using AspireEFCorePgSQLExample.CarsAPI.DTOs;

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
        public async Task<ActionResult<IEnumerable<GetMaker>>> GetMakers()
        {
            var makers = await _context.Makers.ToListAsync();

            try
            {
                var data = makers.Select(x => new GetMaker(x.Guid, x.Name, x.Country));
                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Makers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMaker>> GetMaker(Guid id)
        {
            var maker = await _context.Makers.FindAsync(id);

            if (maker == null)
            {
                return NotFound();
            }

            try
            {
                return Ok(new GetMaker(maker.Guid, maker.Name, maker.Country));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT: api/Makers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaker(Guid id, PutMaker data)
        {
            if (id != data.Guid)
            {
                return BadRequest();
            }

            var maker = await _context.Makers.FindAsync(id);

            if (maker == null)
            {
                return NotFound();
            }

            maker.Name = data.Name;
            maker.Country = data.Country;

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
        public async Task<ActionResult<GetMaker>> PostMaker(PostMaker data)
        {
            var maker = new Maker(Guid.NewGuid(), data.Name, data.Country);

            _context.Makers.Add(maker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            var result = new GetMaker(maker.Guid, maker.Name, maker.Country);

            return CreatedAtAction("GetMaker", new { id = maker.Guid }, result);
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
