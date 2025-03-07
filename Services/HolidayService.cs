using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly ApplicationDbContext _context;

        public HolidayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsHolidayAsync(DateTime date)
        {
            return await _context.Holidays
                .AnyAsync(h => h.Date == date);
        }
        public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
        {
            return await _context.Holidays.ToListAsync();
        }

        public async Task<Holiday> GetHolidayByIdAsync(int id)
        {
            return await _context.Holidays.FindAsync(id);
        }

        public async Task<Holiday> CreateHolidayAsync(Holiday holiday)
        {
            holiday.CreatedDate = DateTime.UtcNow;
            _context.Holidays.Add(holiday);
            await _context.SaveChangesAsync();
            return holiday;
        }

        public async Task<Holiday> UpdateHolidayAsync(Holiday holiday)
        {
            var existingHoliday = await _context.Holidays.FindAsync(holiday.Id);
            if (existingHoliday == null)
            {
                return null;
            }

            existingHoliday.Date = holiday.Date;
            existingHoliday.Description = holiday.Description;
            existingHoliday.UpdatedDate = DateTime.UtcNow;
            existingHoliday.UpdatedBy = holiday.UpdatedBy; // Ensure UpdatedBy is set appropriately

            await _context.SaveChangesAsync();
            return existingHoliday;
        }

        public async Task<bool> DeleteHolidayAsync(int id)
        {
            var existingHoliday = await _context.Holidays.FindAsync(id);
            if (existingHoliday == null)
            {
                return false;
            }

            _context.Holidays.Remove(existingHoliday);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
