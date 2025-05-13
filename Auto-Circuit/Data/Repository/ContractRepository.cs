using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Generics;
using Auto_Circuit.Interfaces;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class ContractRepository(BaseRepository context, IMapper mapper, ICurrentUser currentUser)
{
    public async Task<Contract> GetContactByUser(string userId)
    {
        try
        {
            var contract = await context.GetData<Contract>()
                .Where(x => x.UserId == userId)
                .SingleOrDefaultAsync();
            if (contract is not null)
            {
                return contract;
            }
            else
            {
                throw new Exception("contract is Not Found !");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Contract>> GetContractsAsync()
    {
        try
        {
            var contacts = await context.GetData<Contract>().OrderBy(o => o).ToListAsync();
            return contacts;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
