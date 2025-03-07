using CBZ_OvertTime_Logging.DTOs;

namespace CBZ_OvertTime_Logging.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsBySubsidiaryIdAsync(int subsidiaryId);
        Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task<DepartmentDto> UpdateDepartmentAsync(DepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);
    }
}
