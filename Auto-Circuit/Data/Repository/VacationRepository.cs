using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class VacationRepository(BaseRepository context, IMapper mapper, ICurrentUser currentUser)
{
    public async Task<IEnumerable<Vacation>> GetAllAsync()
    {
        var vacation = await context.GetData<Vacation>()
            .AsNoTracking()
            .ToListAsync();
        return vacation;
    }

    public async Task<IEnumerable<Vacation>> GetVacationsbyUserIdAsync(string id)
    {
        var vacation = await context.GetData<Vacation>()
            .AsNoTracking()
            .Where(v => v.UserId == id)
            .ToListAsync();
        return vacation;
    }

    public async Task<VacationFormDTo> CreateVacationAsync(VacationFormDTo vacationDto)
    {
        var vacation = mapper.Map<Vacation>(vacationDto);
        vacation.UserId = currentUser.UserId;
        await context.AddAsync(vacation);
        return vacationDto;
    }

    public async Task<VacationDTO> UpdateVacationAsync(VacationDTO vacationDto)
    {
        var vacation = await context.GetByIdAsync<Vacation>(vacationDto.Id);
        if (vacation == null)
        {
            throw new Exception("Vacation not found");
        }
        mapper.Map(vacationDto, vacation);
        await context.UpdateAsync(vacation);
        return vacationDto;
    }

    public async Task DeleteVacationAsync(string id)
    {
        var vacation = await context.GetByIdAsync<Vacation>(id);
        if (vacation == null)
        {
            throw new Exception("Vacation not found");
        }
        await context.DeleteAsync<Vacation>(id);
    }

}
