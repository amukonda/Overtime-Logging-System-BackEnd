using Microsoft.AspNetCore.Mvc;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CBZ_OverTime_Logging.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        // GET: api/holiday
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetAllHolidays()
        {
            var holidays = await _holidayService.GetAllHolidaysAsync();
            return Ok(holidays);
        }

        // GET: api/holiday/isHoliday/{date}
        [HttpGet("isHoliday/{date}")]
        public async Task<IActionResult> CheckIfHoliday(DateTime date)
        {
            bool isHoliday = await _holidayService.IsHolidayAsync(date);
            return Ok(new { Date = date, IsHoliday = isHoliday });
        }

        // GET: api/holiday/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Holiday>> GetHoliday(int id)
        {
            var holiday = await _holidayService.GetHolidayByIdAsync(id);
            if (holiday == null)
            {
                return NotFound("Holiday not found.");
            }
            return Ok(holiday);
        }

        // POST: api/holiday
        [HttpPost]
        public async Task<ActionResult<Holiday>> CreateHoliday([FromBody] Holiday holiday)
        {
            if (holiday == null)
            {
                return BadRequest("Holiday is null.");
            }

            var createdHoliday = await _holidayService.CreateHolidayAsync(holiday);
            return CreatedAtAction(nameof(GetHoliday), new { id = createdHoliday.Id }, createdHoliday);
        }

        // PUT: api/holiday/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHoliday(int id, [FromBody] Holiday holiday)
        {
            if (holiday == null || id != holiday.Id)
            {
                return BadRequest("Holiday is null or ID mismatch.");
            }

            var updatedHoliday = await _holidayService.UpdateHolidayAsync(holiday);
            if (updatedHoliday == null)
            {
                return NotFound("Holiday not found.");
            }

            return NoContent();
        }

        // DELETE: api/holiday/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(int id)
        {
            var deleted = await _holidayService.DeleteHolidayAsync(id);
            if (!deleted)
            {
                return NotFound("Holiday not found.");
            }

            return NoContent();
        }
    }
}
