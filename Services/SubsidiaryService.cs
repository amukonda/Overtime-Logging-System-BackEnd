using AutoMapper;
using CBZ_OvertTime_Logging.DatabaseContext;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Interfaces;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.Services
{
    public class SubsidiaryService : ISubsidiaryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SubsidiaryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubsidiaryDto>> GetAllSubsidiariesAsync()
        {
            var subsidiaries = await _context.Subsidiaries.ToListAsync();
            return _mapper.Map<IEnumerable<SubsidiaryDto>>(subsidiaries);
        }

        public async Task<SubsidiaryDto> GetSubsidiaryByIdAsync(int id)
        {
            var subsidiary = await _context.Subsidiaries.FindAsync(id);
            return _mapper.Map<SubsidiaryDto>(subsidiary);
        }

        public async Task<SubsidiaryDto> CreateSubsidiaryAsync(SubsidiaryDto subsidiaryDto)
        {
            var subsidiary = _mapper.Map<Subsidiary>(subsidiaryDto);
            subsidiary.CreatedDate = DateTime.UtcNow;
            subsidiary.UpdatedDate = DateTime.UtcNow;

            _context.Subsidiaries.Add(subsidiary);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubsidiaryDto>(subsidiary);
        }

        public async Task<SubsidiaryDto> UpdateSubsidiaryAsync(SubsidiaryDto subsidiaryDto)
        {
            var subsidiary = await _context.Subsidiaries.FindAsync(subsidiaryDto.Id);
            if (subsidiary == null) return null;

            subsidiary.Name = subsidiaryDto.Name;
            subsidiary.UpdatedDate = DateTime.UtcNow;
            subsidiary.UpdatedBy = subsidiaryDto.UpdatedBy; // Set the updater's ID

            await _context.SaveChangesAsync();
            return _mapper.Map<SubsidiaryDto>(subsidiary);
        }

        public async Task DeleteSubsidiaryAsync(int id)
        {
            var subsidiary = await _context.Subsidiaries.FindAsync(id);
            if (subsidiary != null)
            {
                _context.Subsidiaries.Remove(subsidiary);
                await _context.SaveChangesAsync();
            }
        }
    }
}
