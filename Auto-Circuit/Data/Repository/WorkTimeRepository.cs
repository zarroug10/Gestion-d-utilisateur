using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class WorkTimeRepository(
    BaseRepository context,
    IMapper mapper,
    ICurrentUser currentUser,
    CircuitContext circuitContext)
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

    public async Task<IEnumerable<WorkTimeGroupDto>> GetAllWorkTime()
    {
        try
        {
            var workTime = await context.GetData<WorkTime>()
                                        .Include(x => x.User)
                                        .OrderBy(o => o.StartDate)
                                            .GroupBy(x => new
                                            {
                                                Year = x.StartDate.HasValue ? x.StartDate.Value.Year : 0,
                                                Month = x.StartDate.HasValue ? x.StartDate.Value.Month : 0
                                            })
                                        .Select(g => new WorkTimeGroupDto
                                        {
                                            Year = g.Key.Year,
                                            Month = g.Key.Month,
                                            MonthStatus = g.Any(x => x.IsPending == true)
                                                            ? "Pending"
                                                            : g.Any(x => x.IsApproved == false)
                                                                ? "Rejected"
                                                                : "Approved",
                                            Records = mapper.Map<List<WorkTimeDTo>>(g.ToList()),
                                        })
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

    public async Task AddWorkTime(WorkTimeRequest workTime)
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

    public async Task rejectWorkTime(string id)
    {
        try
        {
            var workTime = await context.GetByIdAsync<WorkTime>(id);
            if (workTime is not null)
            {
                workTime.IsApproved = false;
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

    public async Task<IList<WorkTimeDTo>> GetWorkTimeByUser(string id)
    {
        try
        {
            var User = await context.GetByIdAsync<User>(id);
            if (User is not null)
            {
                var EmployeeTime = await circuitContext.WorkTimes
                                                 .OrderBy(o => o.StartDate)
                                                 .Where(x => x.UserId == User.Id)
                                                 .ProjectTo<WorkTimeDTo>(mapper.ConfigurationProvider)
                                                 .ToListAsync();
                return EmployeeTime;
            }
            else
            {
                throw new Exception(" User is Not Found !");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
