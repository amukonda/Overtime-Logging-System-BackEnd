using AutoMapper;
using CBZ_OvertTime_Logging.DTOs;
using CBZ_OvertTime_Logging.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CBZ_OvertTime_Logging.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Subsidiary, SubsidiaryDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Employees, EmployeeDto>().ReverseMap();
            CreateMap<OvertimeClaim, OvertimeClaimDto>().ReverseMap();
        }
    }
}
