using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DTOs;
using System.Threading.Tasks;

namespace CBZ_OvertTime_Logging.Interfaces
{
    public interface IOvertimeClaimService
    {
        Task<IEnumerable<OvertimeClaimDto>> GetClaimsByEmployeeIdAsync(int employeeId);
        Task<OvertimeClaimDto> CreateClaimAsync(OvertimeClaimDto claimDto);
        Task<OvertimeClaimDto> UpdateClaimAsync(OvertimeClaimDto claimDto);
        Task ApproveClaimAsync(int id, bool isManagerApproval, string comments);

        Task<IEnumerable<vw_OverTimeClaims>> GetAllOverTimeClaimsAsync();
        Task<IEnumerable<vw_OverTimeClaims>> GetFilteredOverTimeClaimsAsync(int userId, int role, string subsidiaryName, string departmentName, string unitName);
    }
}
