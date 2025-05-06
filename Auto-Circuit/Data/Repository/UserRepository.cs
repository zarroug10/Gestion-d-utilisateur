using Auto_Circuit.DTO;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Generics;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class UserRepository(BaseRepository context, IMapper mapper)
{

    public async Task<User> GetUserByIdAsync(string id)
    {
        try
        {
            var user = await context.GetByIdAsync<User>(id);
            if (user is not null)
            {
                return user;
            }
            else
            {
                throw new Exception("User is Not Found !");
            }
        }
        catch
        {
            throw new Exception("Server Error");
        }
    }
    public async Task<IEnumerable<UserDTo>> GetAllUsersAsync()
    {
        return await context.GetData<User>().AsNoTracking().ProjectTo<UserDTo>(mapper.ConfigurationProvider).ToListAsync();
    }
    public async Task AddUserAsync(User user)
    {
        await context.AddAsync(user);
    }
    public async Task UpdateUserAsync(User user)
    {
        await context.UpdateAsync<User>(user);
    }
    public async Task DeleteUserAsync(string id)
    {
        await context.DeleteAsync<User>(id);
    }
}
