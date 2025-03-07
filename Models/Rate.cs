namespace CBZ_OvertTime_Logging.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public string Type { get; set; } // e.g., "Regular", "Sunday", "Holiday"
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }  // User ID of the creator
        public int UpdatedBy { get; set; }  // User ID of the updater
    }
}
