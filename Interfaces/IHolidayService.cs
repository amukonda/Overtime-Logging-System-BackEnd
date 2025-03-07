using CBZ_OvertTime_Logging.Models;

namespace CBZ_OvertTime_Logging.Interfaces
{
    public interface IHolidayService
    {
        Task<bool> IsHolidayAsync(DateTime date);
        Task<IEnumerable<Holiday>> GetAllHolidaysAsync();
        Task<Holiday> GetHolidayByIdAsync(int id);
        Task<Holiday> CreateHolidayAsync(Holiday holiday);
        Task<Holiday> UpdateHolidayAsync(Holiday holiday);
        Task<bool> DeleteHolidayAsync(int id);
    }
}
