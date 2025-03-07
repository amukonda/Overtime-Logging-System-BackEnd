namespace CBZ_OvertTime_Logging.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } // Optional description for the holiday
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater
    }
}
