using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.Services
{
    public class RateService : IRateService
    {
        private readonly ApplicationDbContext _context;

        public RateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal?> GetSundayRateAsync()
        {
            return await _context.Rates
                .Where(r => r.Type == "Sunday")
                .Select(r => r.Amount)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal?> GetHolidayRateAsync()
        {
            return await _context.Rates
                .Where(r => r.Type == "Holiday")
                .Select(r => r.Amount)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal?> GetRegularRateAsync()
        {
            return await _context.Rates
                .Where(r => r.Type == "Regular")
                .Select(r => r.Amount)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rate>> GetAllRatesAsync()
        {
            return await _context.Rates.ToListAsync();
        }

        public async Task<Rate> GetRateByIdAsync(int id)
        {
            return await _context.Rates.FindAsync(id);
        }

        public async Task<Rate> CreateRateAsync(Rate rate)
        {
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
            return rate;
        }

        public async Task<Rate> UpdateRateAsync(Rate rate)
        {
            var existingRate = await _context.Rates.FindAsync(rate.Id);
            if (existingRate == null)
            {
                return null;
            }

            existingRate.Type = rate.Type;
            existingRate.Amount = rate.Amount;
            await _context.SaveChangesAsync();
            return existingRate;
        }

        public async Task<bool> DeleteRateAsync(int id)
        {
            var existingRate = await _context.Rates.FindAsync(id);
            if (existingRate == null)
            {
                return false;
            }

            _context.Rates.Remove(existingRate);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
