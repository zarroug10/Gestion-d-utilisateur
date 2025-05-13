using Auto_Circuit.DTO;
using Auto_Circuit.DTOs;
using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;

using AutoMapper;

namespace Auto_Circuit.Entitymapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<User, UserDTo>()
           .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.ContractId))
           .ForMember(dest => dest.roles, opt => opt.MapFrom(src => src.UserRoles.Select(u => u.Role.Name)));

        CreateMap<LoginDTo, User>();
        CreateMap<UserSignUpDto, User>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract));
        CreateMap<UpdateDTo, User>();

        CreateMap<ContractDto, Contract>();
        CreateMap<Contract, ContractDto>();

        CreateMap<Vacation, VacationDTO>();
        CreateMap<VacationFormDTo, Vacation>();
    }
}
