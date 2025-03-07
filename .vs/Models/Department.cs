namespace CBZ_OvertTime_Logging.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubsidiaryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater
    }
}
