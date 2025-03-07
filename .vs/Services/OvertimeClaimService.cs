using AutoMapper;
using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.Services
{
    public class OvertimeClaimService : IOvertimeClaimService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OvertimeClaimService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OvertimeClaimDto>> GetClaimsByEmployeeIdAsync(int employeeId)
        {
            var claims = await _context.OvertimeClaims
                .Where(c => c.EmployeeId == employeeId).ToListAsync();
            return _mapper.Map<IEnumerable<OvertimeClaimDto>>(claims);
        }

        public async Task<OvertimeClaimDto> CreateClaimAsync(OvertimeClaimDto claimDto)
        {
            var claim = _mapper.Map<OvertimeClaim>(claimDto);
            claim.DateCreated = DateTime.UtcNow;
            claim.CreatedBy = claimDto.CreatedBy; // Set the creator's ID
            claim.UpdatedDate = DateTime.UtcNow;
            claim.UpdatedBy = claimDto.CreatedBy; // Set the updater's ID

          

            try
            {
                _context.OvertimeClaims.Add(claim);
                await _context.SaveChangesAsync();
                         }
            catch (DbUpdateException ex)
            {
                // Log the error details
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            return _mapper.Map<OvertimeClaimDto>(claim);
        }

        public async Task<OvertimeClaimDto> UpdateClaimAsync(OvertimeClaimDto claimDto)
        {
            var claim = await _context.OvertimeClaims.FindAsync(claimDto.Id);
            if (claim == null) return null;

            claim.StartDateTime = claimDto.StartDateTime;
            claim.EndDateTime = claimDto.EndDateTime;
            claim.OvertimeHours = claimDto.OvertimeHours;
            claim.Rate = claimDto.Rate;
            claim.Reason = claimDto.Reason;
            claim.UpdatedDate = DateTime.UtcNow;
            claim.UpdatedBy = claimDto.UpdatedBy; // Set the updater's ID

            await _context.SaveChangesAsync();
            return _mapper.Map<OvertimeClaimDto>(claim);
        }

        public async Task ApproveClaimAsync(int id, bool isManagerApproval, string comments)
        {
            var claim = await _context.OvertimeClaims.FindAsync(id);
            if (claim != null)
            {
                if (isManagerApproval)
                {
                    claim.LineManagerApprovalStatus = true;
                    claim.LineManagerComments = comments;
                    claim.LineManagerName = "Manager Name"; // Replace with actual manager's name
                }
                else
                {
                    claim.HeadApprovalStatus = true;
                    claim.HeadComments = comments;
                    claim.HeadName = "Head Name"; // Replace with actual head's name
                }

                claim.UpdatedDate = DateTime.UtcNow;
                claim.UpdatedBy = 1; // Replace with actual user ID of the approver

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<vw_OverTimeClaims>> GetAllOverTimeClaimsAsync()
        {
            // vw_OverTimeClaims is a view in the database
            return await _context.Set<vw_OverTimeClaims>().ToListAsync();
        }

        public async Task<IEnumerable<vw_OverTimeClaims>> GetFilteredOverTimeClaimsAsync(int userId, int role, string subsidiaryName, string departmentName, string unitName)
    {
        // Ensure all parameters are provided
        if (string.IsNullOrEmpty(subsidiaryName) || 
            string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(unitName))
        {
            throw new ArgumentException("All filter parameters must be provided.");
        }

            if (role == 4)
            {

                return await _context.Set<vw_OverTimeClaims>()
                    .Where(x =>  x.Subsidiary_Name == subsidiaryName )
                    .ToListAsync();
            }

            if (role == 3)
            {

                return await _context.Set<vw_OverTimeClaims>()
                    .Where(x => x.Role == 3 &&
                                x.Employee_Id == userId &&                 
                                x.Subsidiary_Name == subsidiaryName &&
                                x.Department_Name == departmentName &&
                                x.Unit_Name == unitName)
                    .ToListAsync();
            }

            else if (role == 2)
            {

                return await _context.Set<vw_OverTimeClaims>()
                                .Where(x => x.Subsidiary_Name == subsidiaryName &&
                                x.Department_Name == departmentName &&
                                x.Unit_Name == unitName)
                                .ToListAsync();
            }
            else if (role == 1)
            {

                return await _context.Set<vw_OverTimeClaims>()
                                .Where(x => x.Subsidiary_Name == subsidiaryName &&
                                x.Department_Name == departmentName 
                                  )
                                .ToListAsync();
            }
            else
            {
                throw new ArgumentException("No value match filter parameters provided.");
            }

        }
    }
}
