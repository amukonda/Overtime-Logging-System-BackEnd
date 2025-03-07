using CBZ_OverTime_Logging.Interfaces;
using CBZ_OverTime_Logging.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBZ_OverTime_Logging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitsService _unitsService;

        public UnitsController(IUnitsService unitsService)
        {
            _unitsService = unitsService;
        }

        // GET: api/units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Units>>> GetAllUnits()
        {
            var units = await _unitsService.GetAllUnitsAsync();
            return Ok(units);
        }

        // GET: api/units/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Units>> GetUnit(int id)
        {
            var unit = await _unitsService.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound("Unit not found.");
            }
            return Ok(unit);
        }

        // POST: api/units
        [HttpPost]
        public async Task<ActionResult<Units>> CreateUnit([FromBody] Units unit)
        {
            if (unit == null)
            {
                return BadRequest("Unit is null.");
            }

            var createdUnit = await _unitsService.CreateUnitAsync(unit);
            return CreatedAtAction(nameof(GetUnit), new { id = createdUnit.Id }, createdUnit);
        }

        // PUT: api/units/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, [FromBody] Units unit)
        {
            if (unit == null || id != unit.Id)
            {
                return BadRequest("Unit is null or ID mismatch.");
            }

            var updatedUnit = await _unitsService.UpdateUnitAsync(unit);
            if (updatedUnit == null)
            {
                return NotFound("Unit not found.");
            }

            return NoContent();
        }

        // DELETE: api/units/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var deleted = await _unitsService.DeleteUnitAsync(id);
            if (!deleted)
            {
                return NotFound("Unit not found.");
            }

            return NoContent();
        }
    }
}
