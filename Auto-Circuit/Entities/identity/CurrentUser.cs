using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Auto_Circuit.Entities.identity;

public class CurrentUser
{
    private readonly ClaimsPrincipal claimsPrincipal;

    private string id;
    private string email;
    private string? firstName;
    private string? lastName;
    private string? userName;

    private bool? isAuthenticated;
    private bool? isAdmin;
    private bool? isAccount;

    private IList<string>? roles;

    public CurrentUser()
    {

    }

    // Constructor accepting ClaimsPrincipal  
    public CurrentUser(ClaimsPrincipal claimsPrincipal)
          : this()
    {
        this.claimsPrincipal = claimsPrincipal;
    }


    public string Id
    {
        get
        {
            if (claimsPrincipal == null)
            {
                throw new InvalidOperationException("ClaimsPrincipal is not initialized.");
            }

            if (id == null)
            {
                id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Email
    {
        get
        {
            if (email == null)
            {
                email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            }
            return email;
        }
        set
        {
            email = value;
        }
    }

    public string? FirstName
    {
        get
        {
            if (firstName == null)
            {
                firstName = claimsPrincipal.FindFirst("FirstName")?.Value;
            }
            return firstName;
        }
        set
        {
            firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            if (lastName == null)
            {
                lastName = claimsPrincipal.FindFirstValue("LastName");
            }
            return lastName;
        }
        set
        {
            lastName = value;
        }
    }
    public string? Username
    {
        get
        {
            if (userName == null)
            {
                userName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
            }
            return userName;
        }
        set
        {
            userName = value;
        }
    }

    public bool? IsAuthenticated
    {
        get
        {
            isAuthenticated ??= claimsPrincipal?.Identity?.IsAuthenticated;
            return isAuthenticated;
        }
        set
        {
            isAuthenticated = value;
        }
    }

    public bool? IsAdmin
    {
        get
        {
            if (isAdmin == null)
            {
                isAdmin = claimsPrincipal.IsInRole(UserType.Account);
            }
            return isAdmin;
        }
    }

    public bool? IsAccount
    {
        get
        {
            if (isAccount == null)
            {
                isAccount = claimsPrincipal.IsInRole(UserType.Account);
            }
            return isAccount;
        }
        set
        {
            isAccount = value;
        }
    }

    public IList<string> Roles
    {
        get
        {
            if (roles == null)
            {
                roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            }
            return roles;
        }
        set
        {
            roles = value;
        }
    }
}
