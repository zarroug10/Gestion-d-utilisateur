using Auto_Circuit.DTO;
using Auto_Circuit.Entities;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class MonthlySpentRepository(
   BaseRepository baseRepository,
   ICurrentUser currentUser,
   IMapper mapper)
{

    public async Task<IEnumerable<MonthlySpentDTo>> GetSpent()
    {
        var spentList = await baseRepository.GetData<MonthlySpent>().OrderBy(o => o).ProjectTo<MonthlySpentDTo>(mapper.ConfigurationProvider).ToListAsync();
        return spentList;
    }

    public async Task<MonthlySpentDTo> GetSpentById(string id)
    {
        var spent = await baseRepository.GetData<MonthlySpent>()
            .Where(x => x.Id == id)
            .ProjectTo<MonthlySpentDTo>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        return spent;
    }

    public async Task<MonthlySpentDTo> CreateSpent(MonthlySpentDTo spentDTo)
    {
        if (currentUser.UserId == null)
        {
            throw new Exception("there is no user detected !");
        }
        spentDTo.UserId = currentUser.UserId;
        if (await baseRepository.GetData<MonthlySpent>().AnyAsync(x => x.Month == spentDTo.Month && x.Year == spentDTo.Year))
        {
            //add the new amount to the old amount while keeping inside the same record of month and year
            var existingSpent = await baseRepository.GetData<MonthlySpent>().FirstOrDefaultAsync(
                x => x.Month == spentDTo.Month
                && x.Year == spentDTo.Year);

            existingSpent.TotalAmount += spentDTo.TotalAmount;

            // Update the record in the repository
            await baseRepository.UpdateAsync(existingSpent);

            // Map back to DTO for return
            return mapper.Map<MonthlySpentDTo>(existingSpent);
        }
        var spent = mapper.Map<MonthlySpent>(spentDTo);
        await baseRepository.AddAsync(spent);
        return spentDTo;
    }

    public async Task<MonthlySpentDTo> UpdateSpent(string id, MonthlySpentDTo dTo)
    {
        var Selectedspent = await GetSpentById(id);
        var updatedForm = mapper.Map(dTo, Selectedspent);
        await baseRepository.UpdateAsync(updatedForm);
        return dTo;
    }

    public async Task DeleteSpent(string id)
    {
        await baseRepository.DeleteAsync<MonthlySpent>(id);
    }
}
