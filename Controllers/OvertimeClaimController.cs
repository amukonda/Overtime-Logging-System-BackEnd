using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace CBZ_OvertTime_Logging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OvertimeClaimController : ControllerBase
    {
        private readonly IOvertimeClaimService _overtimeClaimService;
        private readonly IRateService _rateService; // New service for rates
        private readonly IHolidayService _holidayService; // New service for holidays

        public OvertimeClaimController(IOvertimeClaimService overtimeClaimService, IRateService rateService, IHolidayService holidayService)
        {
            _overtimeClaimService = overtimeClaimService;
            _rateService = rateService;
            _holidayService = holidayService;
        }

        // GET: api/overtimeclaim/{employeeId}
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetClaimsByEmployeeId(int employeeId)
        {
            var claims = await _overtimeClaimService.GetClaimsByEmployeeIdAsync(employeeId);
            return Ok(claims);
        }
        


        // POST: api/overtimeclaim
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromBody] OvertimeClaimDto claimDto)
        {
            if (claimDto == null)
            {
                return BadRequest("Overtime claim data is null.");
            }

            // Validate StartDateTime
            if (claimDto.StartDateTime == default)
            {
                return BadRequest("Start date and time must be provided.");
            }

            // Validate EndDateTime
            if (claimDto.EndDateTime == default)
            {
                return BadRequest("End date and time must be provided.");
            }

            // Validate that StartDateTime is before EndDateTime
            if (claimDto.StartDateTime >= claimDto.EndDateTime)
            {
                return BadRequest("Start date and time must be before End date and time.");
            }

            // Validate that StartDateTime and EndDateTime are on the same day
            if (claimDto.StartDateTime.Date != claimDto.EndDateTime.Date)
            {
                return BadRequest("Start date and end date must be on the same day.");
            }

            // Validate OvertimeHours
            // Calculate the difference
            TimeSpan timeDifference = claimDto.EndDateTime - claimDto.StartDateTime;

            // Convert the difference to hours as a decimal
            decimal totalHours = (decimal)timeDifference.TotalHours;

            claimDto.OvertimeHours = totalHours;

            if (claimDto.OvertimeHours <= 0)
            {
                return BadRequest("Overtime hours must be greater than zero.");
            }

            // Validate Rate
            if (claimDto.Rate <= 0)
            {
                return BadRequest("Rate must be greater than zero.");
            }

            // Validate Reason
            if (string.IsNullOrWhiteSpace(claimDto.Reason))
            {
                return BadRequest("Reason for the overtime claim must be provided.");
            }

            // Validate CreatedBy
            if (claimDto.CreatedBy <= 0)
            {
                return BadRequest("Creator's user ID must be valid.");
            }

            // If all validations pass, create the claim
            try
            {
                var createdClaim = await _overtimeClaimService.CreateClaimAsync(claimDto);
                return CreatedAtAction(nameof(GetClaimsByEmployeeId), new { employeeId = createdClaim.EmployeeId }, createdClaim);
            }
            catch (Exception ex)
            {
                // Log the exception here (omitted for brevity)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/overtimeclaim
        [HttpPut]
        public async Task<IActionResult> UpdateClaim([FromBody] OvertimeClaimDto claimDto)
        {
            if (claimDto == null)
            {
                return BadRequest("Overtime claim data is null");
            }

            var updatedClaim = await _overtimeClaimService.UpdateClaimAsync(claimDto);
            return Ok(updatedClaim);
        }

        // POST: api/overtimeclaim/approve/{id}
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveClaim(int id, [FromBody] ApproveClaimDto approveDto)
        {
            if (approveDto == null)
            {
                return BadRequest("Approval data is null");
            }

            await _overtimeClaimService.ApproveClaimAsync(id, approveDto.IsManagerApproval, approveDto.Comments);
            return NoContent();
        }

        // GET: api/overtimeclaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<vw_OverTimeClaims>>> GetAllOverTimeClaims()
        {
            var claims = await _overtimeClaimService.GetAllOverTimeClaimsAsync();
            return Ok(claims);
        }

        // GET: api/overtimeclaims/filter
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<vw_OverTimeClaims>>> GetFilteredOverTimeClaims(
            [FromQuery] int userId,
            [FromQuery] int role,
            [FromQuery] string subsidiaryName,
            [FromQuery] string departmentName,
            [FromQuery] string unitName)
        {
            try
            {
                var claims = await _overtimeClaimService.GetFilteredOverTimeClaimsAsync(userId,role, subsidiaryName, departmentName, unitName);
                return Ok(claims);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
