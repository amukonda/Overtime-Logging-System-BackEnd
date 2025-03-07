using AutoMapper;
using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsBySubsidiaryIdAsync(int subsidiaryId)
        {
            var departments = await _context.Departments
                .Where(d => d.SubsidiaryId == subsidiaryId).ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            department.CreatedDate = DateTime.UtcNow;
            department.UpdatedDate = DateTime.UtcNow;

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(departmentDto.Id);
            if (department == null) return null;

            department.Name = departmentDto.Name;
            department.UpdatedDate = DateTime.UtcNow;
            department.UpdatedBy = departmentDto.UpdatedBy; // Set the updater's ID

            await _context.SaveChangesAsync();
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
