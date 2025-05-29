using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class WorkTimeRepository(BaseRepository context, IMapper mapper, ICurrentUser currentUser)
{

    public async Task<IEnumerable<WorkTimeDTo>> GetWorkTimeByUserAsync()
    {
        try
        {
            var WorkTime = await context.GetData<WorkTime>()
                                        .Where(x => x.UserId == currentUser.UserId)
                                        .ProjectTo<WorkTimeDTo>(mapper.ConfigurationProvider)
                                        .ToListAsync();
            if (WorkTime is not null)
            {
                return WorkTime;
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

    public async Task<IEnumerable<WorkTimeDTo>> GetAllWorkTime()
    {
        try
        {
            var workTime = await context.GetData<WorkTime>()
                                        .OrderBy(o => o.StartDate)
                                        .ProjectTo<WorkTimeDTo>(mapper.ConfigurationProvider)
                                        .ToListAsync();
            if (workTime is not null)
            {
                return workTime;
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

    public async Task<WorkTime> GetWorkTimeById(string id)
    {
        try
        {
            var workTime = await context.GetByIdAsync<WorkTime>(id);
            if (workTime is not null)
            {
                return workTime;
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

    public async Task AddWorkTime(WorkTimeDTo workTime)
    {
        try
        {
            var work = mapper.Map<WorkTime>(workTime);

            await context.AddAsync(work);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task UpdateWorkTime(WorkTimeDTo workTime, string id)
    {
        try
        {
            var record = await GetWorkTimeById(id);
            var work = mapper.Map(workTime, record);

            await context.UpdateAsync(work);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteWorkTime(string id)
    {
        try
        {
            await context.DeleteAsync<WorkTime>(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task ApproveWorkTime(string id)
    {
        try
        {
            var workTime = await context.GetByIdAsync<WorkTime>(id);
            if (workTime is not null)
            {
                workTime.IsApproved = true;
                workTime.IsPending = false;
                await context.UpdateAsync(workTime);
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
