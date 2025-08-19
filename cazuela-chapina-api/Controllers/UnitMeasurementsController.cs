using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cazuela_chapina_api.Context;
using cazuela_chapina_api.Models;

namespace cazuela_chapina_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitMeasurementsController : ControllerBase
    {
        private readonly AppDbC _context;

        public UnitMeasurementsController(AppDbC context)
        {
            _context = context;
        }

        // GET: api/UnitMeasurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitMeasurement>>> Getunit_measurement()
        {
            return await _context.unit_measurement.ToListAsync();
        }

        // GET: api/UnitMeasurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitMeasurement>> GetUnitMeasurement(int id)
        {
            var unitMeasurement = await _context.unit_measurement.FindAsync(id);

            if (unitMeasurement == null)
            {
                return NotFound();
            }

            return unitMeasurement;
        }

        // PUT: api/UnitMeasurements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitMeasurement(int id, UnitMeasurement unitMeasurement)
        {
            if (id != unitMeasurement.UnitMeasurementId)
            {
                return BadRequest();
            }

            _context.Entry(unitMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitMeasurementExists(id))
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

        // POST: api/UnitMeasurements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitMeasurement>> PostUnitMeasurement(UnitMeasurement unitMeasurement)
        {
            _context.unit_measurement.Add(unitMeasurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnitMeasurement", new { id = unitMeasurement.UnitMeasurementId }, unitMeasurement);
        }

        // DELETE: api/UnitMeasurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitMeasurement(int id)
        {
            var unitMeasurement = await _context.unit_measurement.FindAsync(id);
            if (unitMeasurement == null)
            {
                return NotFound();
            }

            _context.unit_measurement.Remove(unitMeasurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitMeasurementExists(int id)
        {
            return _context.unit_measurement.Any(e => e.UnitMeasurementId == id);
        }
    }
}
