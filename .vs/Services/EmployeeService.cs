using AutoMapper;
using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartmentIdAsync(int UnitId)
        {
            var employees = await _context.Employees
                .Where(e => e.UnitId == UnitId).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employees>(employeeDto);
            employee.CreatedDate = DateTime.UtcNow;
            employee.UpdatedDate = DateTime.UtcNow;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(employeeDto.Id);
            if (employee == null) return null;

            employee.Name = employeeDto.Name;
            employee.Role = employeeDto.Role;
            employee.UpdatedDate = DateTime.UtcNow;
            employee.UpdatedBy = employeeDto.UpdatedBy; // Set the updater's ID

            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<vw_Employees>> GetAllEmployeesAsync()
        {
            return await _context.Set<vw_Employees>().ToListAsync();
        }

        public async Task<IEnumerable<vw_Employees>> GetFilteredEmployeesAsync(string role, string subsidiaryName, string departmentName, string unitName)
        {
            // Ensure all parameters are provided
            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(subsidiaryName) ||
                string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(unitName))
            {
                throw new ArgumentException("All filter parameters must be provided.");
            }

            return await _context.Set<vw_Employees>()
                .Where(x => x.Role == role &&
                            x.Subsidiary_Name == subsidiaryName &&
                            x.Department_Name == departmentName &&
                            x.Unit_Name == unitName)
                .ToListAsync();
        }
    }
}
