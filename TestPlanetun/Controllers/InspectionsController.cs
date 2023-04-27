using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPlanetun.Models;
using Microsoft.AspNetCore.Authorization;
using TestPlanetun.Context;

namespace TestPlanetun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class InspectionsController : ControllerBase
    {
        private readonly testPlanetunContext _context;

        public InspectionsController(testPlanetunContext context)
        {
            _context = context;
        }

        // GET: api/Inspections
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Inspection>>> GetInspection()
        {
            return await _context.Inspection.ToListAsync();
        }

        // GET: api/Inspections/5
        [HttpGet("{id}"), Authorize]
        [Authorize]
        public async Task<ActionResult<Inspection>> GetInspection(int id)
        {
            var inspection = await _context.Inspection.FindAsync(id);

            if (inspection == null)
            {
                return NotFound();
            }

            return inspection;
        }

        // PUT: api/Inspections/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInspection(int id, Inspection inspection)
        {
            if (id != inspection.CompanyId)
            {
                return BadRequest();
            }

            _context.Entry(inspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
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

        // POST: api/Inspections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Inspection>> PostInspection(Inspection inspection)
        {
            _context.Inspection.Add(inspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspection", new { id = inspection.CompanyId }, inspection);
        }

        // DELETE: api/Inspections/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Inspection>> DeleteInspection(int id)
        {
            var inspection = await _context.Inspection.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }

            _context.Inspection.Remove(inspection);
            await _context.SaveChangesAsync();

            return inspection;
        }

        private bool InspectionExists(int id)
        {
            return _context.Inspection.Any(e => e.CompanyId == id);
        }
    }
}
