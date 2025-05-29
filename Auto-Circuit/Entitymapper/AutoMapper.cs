using Auto_Circuit.DTO;
using Auto_Circuit.DTOs;
using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Interfaces;

using AutoMapper;

namespace Auto_Circuit.Entitymapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<User, UserDTo>()
           .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.ContractId))
           .ForMember(dest => dest.roles, opt => opt.MapFrom(src => src.UserRoles.Select(u => u.Role.Name)))
           .ForMember(dest => dest.vacation, opt => opt.MapFrom(src => src.Vacations));


        CreateMap<LoginDTo, User>();
        CreateMap<UserSignUpDto, User>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Contract));
        CreateMap<UpdateDTo, User>();

        CreateMap<ContractDto, Contract>();
        CreateMap<Contract, ContractDto>();

        CreateMap<Vacation, VacationDTO>();
        CreateMap<VacationFormDTo, Vacation>();

        CreateMap<MonthlySpent, MonthlySpentDTo>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        CreateMap<MonthlySpentDTo, MonthlySpent>();

        CreateMap<WorkTime, WorkTimeDTo>()
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.User.UserName));

        CreateMap<WorkTimeDTo, WorkTime>();
        CreateMap<WorkTime, WorkTimeDTo>();
    }
}
