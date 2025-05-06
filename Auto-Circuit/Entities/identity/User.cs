using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace Auto_Circuit.Entities.identity;

public class User : IdentityUser<string>
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<UserProducts> UserProducts { get; set; } = [];
    public ICollection<UserRole> UserRoles { get; set; } = [];

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
