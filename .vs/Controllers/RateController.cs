using Microsoft.AspNetCore.Mvc;
using CBZ_OvertTime_Logging.Interfaces;
using System.Threading.Tasks;
using CBZ_OvertTime_Logging.Models;
namespace CBZ_OverTime_Logging.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;
        private readonly IHolidayService _holidayService;

        public RateController(IRateService rateService, IHolidayService holidayService)
        {
            _rateService = rateService;
            _holidayService = holidayService;
        }

        // GET: api/overtimeclaim/rate/{startDateTime}
        [HttpGet("rateType_and_rate/{startDateTime}")]
        public async Task<IActionResult> GetRateForDate(DateTime startDateTime)
        {
            // Check if the date is a Sunday
            if (startDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                var sundayRate = await _rateService.GetSundayRateAsync();
                if (sundayRate == null)
                {
                    return NotFound("Sunday rate not found.");
                }

                return Ok(new { RateType = "Sunday", Rate = sundayRate });
            }

            // Check if the date is a holiday
            var isHoliday = await _holidayService.IsHolidayAsync(startDateTime.Date);
            if (isHoliday)
            {
                var holidayRate = await _rateService.GetHolidayRateAsync();
                if (holidayRate == null)
                {
                    return NotFound("Holiday rate not found.");
                }

                return Ok(new { RateType = "Holiday", Rate = holidayRate });
            }

            // If it's neither a Sunday nor a holiday, return the regular rate
            var regularRate = await _rateService.GetRegularRateAsync();
            if (regularRate == null)
            {
                return NotFound("Regular rate not found.");
            }

            return Ok(new { RateType = "Regular", Rate = regularRate });
        }


        // GET: api/rate/{endDateTime}
        [HttpGet("{endDateTime}")]
        public async Task<IActionResult> GetRate(DateTime endDateTime)
        {
            decimal? rate = null;
            string rateType = null;

            // Check if the date is a Sunday
            if (endDateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                rate = await _rateService.GetSundayRateAsync();
                rateType = "Sunday";
            }
            else
            {
                // Check if the date is a holiday
                bool isHoliday = await _holidayService.IsHolidayAsync(endDateTime.Date);
                if (isHoliday)
                {
                    rate = await _rateService.GetHolidayRateAsync();
                    rateType = "Holiday";
                }
                else
                {
                    rate = await _rateService.GetRegularRateAsync();
                    rateType = "Regular";
                }
            }

            if (rate == null)
            {
                return NotFound($"{rateType} rate not found.");
            }

            return Ok(new { RateType = rateType, Rate = rate });
        }
        // GET: api/rate/sunday
        [HttpGet("sunday")]
        public async Task<IActionResult> GetSundayRate()
        {
            var rate = await _rateService.GetSundayRateAsync();
            if (rate == null)
            {
                return NotFound("Sunday rate not found.");
            }

            return Ok(new { RateType = "Sunday", Rate = rate });
        }

        // GET: api/rate/holiday
        [HttpGet("holiday")]
        public async Task<IActionResult> GetHolidayRate()
        {
            var rate = await _rateService.GetHolidayRateAsync();
            if (rate == null)
            {
                return NotFound("Holiday rate not found.");
            }

            return Ok(new { RateType = "Holiday", Rate = rate });
        }

        // GET: api/rate/regular
        [HttpGet("regular")]
        public async Task<IActionResult> GetRegularRate()
        {
            var rate = await _rateService.GetRegularRateAsync();
            if (rate == null)
            {
                return NotFound("Regular rate not found.");
            }

            return Ok(new { RateType = "Regular", Rate = rate });
        }

        // GET: api/rate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetAllRates()
        {
            var rates = await _rateService.GetAllRatesAsync();
            return Ok(rates);
        }

        // GET: api/rate/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Rate>> GetRate(int id)
        {
            var rate = await _rateService.GetRateByIdAsync(id);
            if (rate == null)
            {
                return NotFound("Rate not found.");
            }
            return Ok(rate);
        }

        // POST: api/rate
        [HttpPost]
        public async Task<ActionResult<Rate>> CreateRate([FromBody] Rate rate)
        {
            if (rate == null)
            {
                return BadRequest("Rate is null.");
            }

            var createdRate = await _rateService.CreateRateAsync(rate);
            return CreatedAtAction(nameof(GetRate), new { id = createdRate.Id }, createdRate);
        }

        // PUT: api/rate/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRate(int id, [FromBody] Rate rate)
        {
            if (rate == null || id != rate.Id)
            {
                return BadRequest("Rate is null or ID mismatch.");
            }

            var updatedRate = await _rateService.UpdateRateAsync(rate);
            if (updatedRate == null)
            {
                return NotFound("Rate not found.");
            }

            return NoContent();
        }

        // DELETE: api/rate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRate(int id)
        {
            var deleted = await _rateService.DeleteRateAsync(id);
            if (!deleted)
            {
                return NotFound("Rate not found.");
            }

            return NoContent();
        }
    }
}
