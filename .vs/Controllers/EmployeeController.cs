using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Services;
using Microsoft.AspNetCore.Mvc;

namespace CBZ_OvertTime_Logging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employee/{departmentId}
        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartmentId(int departmentId)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
            return Ok(employees);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null");
            }

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeesByDepartmentId), new { departmentId = createdEmployee.UnitId }, createdEmployee);
        }

        // PUT: api/employee
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null");
            }

            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employeeDto);
            return Ok(updatedEmployee);
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<vw_Employees>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/employees/filter
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<vw_Employees>>> GetFilteredEmployees(
            [FromQuery] string role,
            [FromQuery] string subsidiaryName,
            [FromQuery] string departmentName,
            [FromQuery] string unitName)
        {
            try
            {
                var employees = await _employeeService.GetFilteredEmployeesAsync(role, subsidiaryName, departmentName, unitName);
                return Ok(employees);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
