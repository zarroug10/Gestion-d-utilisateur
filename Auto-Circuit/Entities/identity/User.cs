using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

using Auto_Circuit.Entities.Enum;

using Microsoft.AspNetCore.Identity;

namespace Auto_Circuit.Entities.identity;

public class User : IdentityUser<string>
{
    public int Cin { get; set; }
    public string Gender { get; set; }
    public string Status { get; set; }
    public int? kidsNumber { get; set; }
    public Contract ContractId { get; set; }
    public IList<WorkTime> WorkTime { get; set; }
    public IList<Vacation> Vacations { get; set; }
    public IList<UserRole> UserRoles { get; set; } = [];

    public User()
    {
        Id = Guid.NewGuid().ToString();
    }

    public User(string userName, string email)
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpper();
        Email = email;
        NormalizedEmail = email?.ToUpper();
        EmailConfirmed = false;
        LockoutEnabled = true;
        AccessFailedCount = 0;
    }
}
