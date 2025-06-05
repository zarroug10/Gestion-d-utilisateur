using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class VacationRepository(BaseRepository context, IMapper mapper, ICurrentUser currentUser)
{
    public async Task<IEnumerable<VacationDTO>> GetAllAsync()
    {
        var vacation = await context.GetData<Vacation>()
            .OrderByDescending(x => x.IsPending)
                .ThenBy(x => x.IsApproved)
                .ThenBy(o => o.StartDate)
            .AsNoTracking()
            .ProjectTo<VacationDTO>(mapper.ConfigurationProvider)
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
        await context.DeleteAsync<Vacation>(id);
    }

    public async Task ApproveVacation(string id)
    {
        try
        {
            var vacation = await context.GetByIdAsync<Vacation>(id);
            if (vacation is not null)
            {
                vacation.IsApproved = true;
                vacation.IsPending = false;
                await context.UpdateAsync(vacation);
            }
            else
            {
                throw new Exception("WorkTime is Not Found !");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task RejectVacation(string id)
    {
        try
        {
            var vacation = await context.GetByIdAsync<Vacation>(id);
            if (vacation is not null)
            {
                vacation.IsApproved = false;
                vacation.IsPending = false;
                await context.UpdateAsync(vacation);
            }
            else
            {
                throw new Exception("WorkTime is Not Found !");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
