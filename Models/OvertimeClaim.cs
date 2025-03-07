namespace CBZ_OvertTime_Logging.Models
{
    public class OvertimeClaim
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
        public int CreatedBy { get; set; }  // User ID of the creator
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }  // User ID of the updater
    }
}
