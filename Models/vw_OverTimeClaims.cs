namespace CBZ_OverTime_Logging.Models
{
    public class vw_OverTimeClaims
    {
        public int Id { get; set; }
               
       
        public int UserConnect_Id { get; set; }
        public string? Employee_Name { get; set; }
        public int? Role { get; set; }
        public string? Subsidiary_Name { get; set; }
        public string? Department_Name { get; set; }
        public string? Unit_Name { get; set; }
        public int Employee_Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal Rate { get; set; }
        public string Reason { get; set; }
        public bool LineManagerApprovalStatus { get; set; }
          public string LineManagerComments { get; set; }
        public bool HeadApprovalStatus { get; set; }
        public string HeadComments { get; set; }
       

    }
}
