using Auto_Circuit.DTO;
using Auto_Circuit.Entities.identity;
using Auto_Circuit.Generics;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data.Repository;

public class UserRepository(BaseRepository context, IMapper mapper, CircuitContext circuitContext)
{
    public async Task<UserDTo> GetUserByIdAsync(string id)
    {
        try
        {
            var user = await circuitContext.Users
                                           .Include(u => u.ContractId)
                                           .Include(u => u.Vacations)
                                           .AsSplitQuery()
                                           .ProjectTo<UserDTo>(mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(x => x.id == id);
            if (user is not null)
            {
                return user;
            }
            else
            {
                throw new Exception("User is Not Found !");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<IEnumerable<UserDTo>> GetAllUsersAsync()
    {
        // Get basic user data without the roles
        var users = await context.GetData<User>()
            .AsNoTracking()
            .Include(u => u.ContractId)
            .Include(u => u.Vacations)
            .AsSplitQuery()
            .ProjectTo<UserDTo>(mapper.ConfigurationProvider)
            .ToListAsync();

        // Use raw SQL to query the role relationships
        string roleSql = @"
        SELECT ur.UserId, r.Name as RoleName 
        FROM AspNetUserRoles ur 
        JOIN AspNetRoles r ON ur.RoleId = r.Id";

        var userRoleResults = new List<UserRoleResult>();

        // Use connection directly for diagnostic purposes
        using (var command = circuitContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = roleSql;

            if (command.Connection.State != System.Data.ConnectionState.Open)
                await command.Connection.OpenAsync();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    userRoleResults.Add(new UserRoleResult
                    {
                        UserId = reader.GetString(0),
                        RoleName = reader.GetString(1)
                    });
                }
            }
        }

        Console.WriteLine($"Raw SQL query returned {userRoleResults.Count} user role records");

        // Group roles by userId
        var rolesByUser = userRoleResults
            .GroupBy(ur => ur.UserId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(ur => ur.RoleName).ToList()
            );

        // Join users with their roles
        foreach (var user in users)
        {
            if (rolesByUser.TryGetValue(user.id, out var roles))
            {
                user.roles = roles;
            }
            else
            {
                user.roles = new List<string>();
            }
        }

        return users;
    }

    public async Task AddUserAsync(User user)
    {
        await context.AddAsync(user);
    }
    public async Task UpdateUserAsync(User user)
    {
        await context.UpdateAsync(user);
    }
    public async Task DeleteUserAsync(string id)
    {
        await context.DeleteAsync<User>(id);
    }
}
