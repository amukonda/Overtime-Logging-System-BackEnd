using CBZ_OverTime_Logging.Interfaces;
using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OverTime_Logging.Services
{
    public class UnitsService : IUnitsService
    {
        private readonly ApplicationDbContext _context;

        public UnitsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Units>> GetAllUnitsAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Units> GetUnitByIdAsync(int id)
        {
            return await _context.Units.FindAsync(id);
        }

        public async Task<Units> CreateUnitAsync(Units unit)
        {
            unit.CreatedDate = DateTime.UtcNow;
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task<Units> UpdateUnitAsync(Units unit)
        {
            var existingUnit = await _context.Units.FindAsync(unit.Id);
            if (existingUnit == null)
            {
                return null;
            }

            existingUnit.Name = unit.Name;
            existingUnit.DepartmentId = unit.DepartmentId;
            existingUnit.UpdatedDate = DateTime.UtcNow;
            existingUnit.UpdatedBy = unit.UpdatedBy; // Ensure UpdatedBy is set appropriately

            await _context.SaveChangesAsync();
            return existingUnit;
        }

        public async Task<bool> DeleteUnitAsync(int id)
        {
            var existingUnit = await _context.Units.FindAsync(id);
            if (existingUnit == null)
            {
                return false;
            }

            _context.Units.Remove(existingUnit);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
