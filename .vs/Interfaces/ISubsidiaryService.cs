using CBZ_OvertTime_Logging.DTOs;

namespace CBZ_OvertTime_Logging.Interfaces
{
    public interface ISubsidiaryService
    {
        Task<IEnumerable<SubsidiaryDto>> GetAllSubsidiariesAsync();
        Task<SubsidiaryDto> GetSubsidiaryByIdAsync(int id);
        Task<SubsidiaryDto> CreateSubsidiaryAsync(SubsidiaryDto subsidiaryDto);
        Task<SubsidiaryDto> UpdateSubsidiaryAsync(SubsidiaryDto subsidiaryDto);
        Task DeleteSubsidiaryAsync(int id);
    }
}
