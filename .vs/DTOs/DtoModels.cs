using CBZ_OvertTime_Logging.Models;

namespace CBZ_OvertTime_Logging.DTOs
{
    public class DtoModels
    {
    }
    public class SubsidiaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater

    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubsidiaryId { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater
    }

    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public int UnitId { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater
    }

    public class OvertimeClaimDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal Rate { get; set; }
        public string Reason { get; set; }
        public bool LineManagerApprovalStatus { get; set; }
        public string LineManagerName { get; set; }
        public string LineManagerComments { get; set; }
        public bool HeadApprovalStatus { get; set; }
        public string HeadComments { get; set; }
        public string HeadName { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class ApproveClaimDto
    {
        public bool IsManagerApproval { get; set; }
        public string Comments { get; set; }
    }
}
