using AutoMapper;
using Employee.Management.System.DTOS;
using Employee.Management.System.Models;

namespace Employee.Management.System.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employe>()
             
            .ReverseMap()
             .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.ID));


         }
    }
}
