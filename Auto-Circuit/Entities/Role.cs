using Microsoft.AspNetCore.Identity;

namespace Auto_Circuit.Entities;

public class Role : IdentityRole<string>
{
    public Role()
    { }

    public Role(string roleName)
    {
        Name = roleName;
        NormalizedName = roleName.ToUpper();
    }
}
