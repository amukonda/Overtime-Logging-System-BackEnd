using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DTOs;

namespace CBZ_OvertTime_Logging.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentIdAsync(int departmentId);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);
        Task<IEnumerable<vw_Employees>> GetAllEmployeesAsync();
        Task<IEnumerable<vw_Employees>> GetFilteredEmployeesAsync(string role, string subsidiaryName, string departmentName, string unitName);
    }
}
