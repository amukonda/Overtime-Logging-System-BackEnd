using CBZ_OvertTime_Logging.Models;

namespace CBZ_OvertTime_Logging.Interfaces
{
    public interface IRateService
    {
        Task<decimal?> GetSundayRateAsync();
        Task<decimal?> GetHolidayRateAsync();
        Task<decimal?> GetRegularRateAsync();
        Task<IEnumerable<Rate>> GetAllRatesAsync();
        Task<Rate> GetRateByIdAsync(int id);
        Task<Rate> CreateRateAsync(Rate rate);
        Task<Rate> UpdateRateAsync(Rate rate);
        Task<bool> DeleteRateAsync(int id);


    }
}
