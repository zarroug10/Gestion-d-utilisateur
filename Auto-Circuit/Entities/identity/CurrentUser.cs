using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;

using Auto_Circuit.Entities.Enum;

namespace Auto_Circuit.Entities.identity;

public class CurrentUser
{
    private readonly ClaimsPrincipal claimsPrincipal;

    private string id;
    private string email;
    private string? cin;
    private string? gender;
    private string? status;
    private string? userName;

    private bool? isAuthenticated;
    private bool? isAdmin;
    private bool? isRH;
    private bool? isEmployee;
    private bool? isResponsible;
    private bool? isDirecteur;

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

    public string CIN
    {
        get
        {
            if (cin == null)
            {
                cin = claimsPrincipal.FindFirst("Cin")?.Value;
            }
            return cin;
        }
        set
        {
            cin = value;
        }
    }
    public string Status
    {
        get
        {
            if (status == null)
            {
                status = claimsPrincipal.FindFirst("Status")?.Value;
            }
            return status;
        }
        set
        {
            status = value;
        }
    }

    public string Gender
    {
        get
        {
            if (gender == null)
            {
                gender = claimsPrincipal.FindFirstValue("Gender");
            }
            return gender;
        }
        set
        {
            gender = value;
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
                isAdmin = claimsPrincipal.IsInRole(UserType.Admin);
            }
            return isAdmin;
        }
    }

    public bool? IsRH
    {
        get
        {
            if (isRH == null)
            {
                isRH = claimsPrincipal.IsInRole(UserType.RH);
            }
            return isRH;
        }
    }

    public bool? IsResponsable
    {
        get
        {
            if (isResponsible == null)
            {
                isResponsible = claimsPrincipal.IsInRole(UserType.Responsable);
            }
            return isResponsible;
        }
    }
    public bool? IsDirecteur
    {
        get
        {
            if (isDirecteur == null)
            {
                isDirecteur = claimsPrincipal.IsInRole(UserType.Directeur);
            }
            return isDirecteur;
        }
    }
    public bool? IsEmployee
    {
        get
        {
            if (isEmployee == null)
            {
                isEmployee = claimsPrincipal.IsInRole(UserType.Employe);
            }
            return isEmployee;
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
