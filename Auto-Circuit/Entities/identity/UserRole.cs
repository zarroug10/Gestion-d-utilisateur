using Microsoft.AspNetCore.Identity;

namespace Auto_Circuit.Entities.identity;

public class UserRole : IdentityUserRole<string>
{
    public User User { get; private set; }
    public Role Role { get; private set; }
}
