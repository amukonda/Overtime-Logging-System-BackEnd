using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CBZ_OvertTime_Logging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/department/{subsidiaryId}
        [HttpGet("{subsidiaryId}")]
        public async Task<IActionResult> GetDepartmentsBySubsidiaryId(int subsidiaryId)
        {
            var departments = await _departmentService.GetDepartmentsBySubsidiaryIdAsync(subsidiaryId);
            return Ok(departments);
        }

        // POST: api/department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Department data is null");
            }

            var createdDepartment = await _departmentService.CreateDepartmentAsync(departmentDto);
            return CreatedAtAction(nameof(GetDepartmentsBySubsidiaryId), new { subsidiaryId = createdDepartment.SubsidiaryId }, createdDepartment);
        }

        // PUT: api/department
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Department data is null");
            }

            var updatedDepartment = await _departmentService.UpdateDepartmentAsync(departmentDto);
            return Ok(updatedDepartment);
        }

        // DELETE: api/department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}
