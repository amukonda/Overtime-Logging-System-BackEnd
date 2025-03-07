using System.ComponentModel.DataAnnotations;

namespace CBZ_OverTime_Logging.Models
{
    public class vw_Employees
    {

        [Key]
        public int Id { get; set; }
        public int UserConnect_Id { get; set; }
        public string? Employee_Name { get; set; }
        public string? Role { get; set; }
        public string? Subsidiary_Name { get; set; }
        public string? Department_Name { get; set; }
        public string? Unit_Name { get; set; }
    }
}
