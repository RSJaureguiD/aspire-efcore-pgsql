using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspireEFCorePgSQLExample.CarsAPI.DAL;
using AspireEFCorePgSQLExample.CarsAPI.DAL.Models;
using AspireEFCorePgSQLExample.CarsAPI.DTOs;

namespace AspireEFCorePgSQLExample.CarsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsDbContext _context;

        public CarsController(CarsDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCar>>> GetCars()
        {
            var cars = await _context.Cars.Include(x => x.Maker).ToListAsync();

            try
            {
                var data = cars.Select(x => new GetCar(x.Guid, x.Name, x.ReleaseYear, new GetMaker(x.Maker!.Guid, x.Maker!.Name, x.Maker!.Country)));
                return Ok(data.ToList());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCar>> GetCar(Guid id)
        {
            var car = await _context.Cars.Include(x => x.Maker).Where(p => p.Guid == id).FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound();
            }

            try
            {
                return Ok(new GetCar(car.Guid, car.Name, car.ReleaseYear, new GetMaker(car.Maker!.Guid, car.Maker!.Name, car.Maker!.Country)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the car with ID {CarId}.", id);
                return Problem("An unexpected error occurred. Please try again later.");
            }

        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, PutCar data)
        {
            if (id != data.Guid)
            {
                return BadRequest();
            }

            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            car.Name = data.Name;
            car.ReleaseYear = data.ReleaseYear;
            car.MakerGuid = data.MakerGuid;

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCar>> PostCar(PostCar data)
        {
            var car = new Car(Guid.NewGuid(), data.Name, data.ReleaseYear, data.MakerGuid);

            _context.Cars.Add(car);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            var newCar = await _context.Cars.Include(x => x.Maker).Where(p => p.Guid == car.Guid).FirstOrDefaultAsync();

            if (newCar is null)
            {
                return Problem("We couldn't find the created car");
            }

            var result = new GetCar(newCar.Guid, newCar.Name, newCar.ReleaseYear, new GetMaker(newCar.Maker!.Guid, newCar.Maker!.Name, newCar.Maker!.Country));

            return CreatedAtAction("GetCar", new { id = car.Guid }, result);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Guid == id);
        }
    }
}
