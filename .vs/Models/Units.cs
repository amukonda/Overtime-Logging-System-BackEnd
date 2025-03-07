using System.ComponentModel.DataAnnotations;

namespace CBZ_OverTime_Logging.Models
{
    public class Units
    {
        [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public int DepartmentId { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
            public int CreatedBy { get; set; }  // User ID of the creator
            public int UpdatedBy { get; set; }  // User ID of the updater
     
    }
}
