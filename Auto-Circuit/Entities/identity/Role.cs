using Microsoft.AspNetCore.Identity;

namespace Auto_Circuit.Entities.identity;

public class Role : IdentityRole<string>
{
    public Role()
    { }

    public Role(string roleName,string id)
    {
        Id = id;
        Name = roleName;
        NormalizedName = roleName.ToUpper();
    }
}
