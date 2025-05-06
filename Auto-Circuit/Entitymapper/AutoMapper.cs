using Auto_Circuit.DTO;
using Auto_Circuit.DTOs;
using Auto_Circuit.Entities.identity;

using AutoMapper;

namespace Auto_Circuit.Entitymapper;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<User, UserDTo>();
        CreateMap<LoginDTo, User>();
        CreateMap<UserSignUpDto, User>();
        CreateMap<UpdateDTo, User>();
    }
}
