using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CBZ_OvertTime_Logging.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        public int UserConnectId { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public int UnitId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater
    }
    public enum Role
    {
        Head,
        Manager,
        Employee
    }
}
