using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Auto_Circuit.Entities.identity;

public class ApplicationClaimsFactory(
   UserManager<User> userManager,
   RoleManager<Role> roleManager,
   IOptions<IdentityOptions> options
   ) : UserClaimsPrincipalFactory<User, Role>(userManager, roleManager, options)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        if (!string.IsNullOrEmpty(user.Cin.ToString()))
        {
            identity.AddClaim(new Claim("Cin", user.Cin.ToString()));
        }

        if (!string.IsNullOrEmpty(user.Status.ToString()))
        {
            identity.AddClaim(new Claim("Status", user.Status.ToString()));
        }

        if (!string.IsNullOrEmpty(user.kidsNumber.ToString()))
        {
            identity.AddClaim(new Claim("KidsNumber", user.kidsNumber.ToString()));
        }

        if (!string.IsNullOrEmpty(user.Gender.ToString()))
        {
            identity.AddClaim(new Claim("Gender", user.Gender.ToString()));
        }

        if (!string.IsNullOrEmpty(user.Status.ToString()))
        {
            identity.AddClaim(new Claim("Status", user.Status.ToString()));
        }

        return identity;
    }
}
