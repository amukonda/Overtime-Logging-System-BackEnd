using CBZ_OverTime_Logging.Models;

namespace CBZ_OverTime_Logging.Interfaces
{
    public interface IUnitsService
    {
        Task<IEnumerable<Units>> GetAllUnitsAsync();
        Task<Units> GetUnitByIdAsync(int id);
        Task<Units> CreateUnitAsync(Units unit);
        Task<Units> UpdateUnitAsync(Units unit);
        Task<bool> DeleteUnitAsync(int id);
    }
}
