using Microsoft.AspNetCore.Identity;

namespace Auto_Circuit.Entities.identity;

public class UserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}
