using Auto_Circuit.Entities.identity;
using Auto_Circuit.Interfaces;

namespace Auto_Circuit.Services;

public class CurrentUserService : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private CurrentUser? _currentUser;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser User
    {
        get
        {
            if (_currentUser == null)
            {
                RefreshUser();
            }
            return _currentUser!;
        }
    }

    public string UserId => User.Id;

    public void RefreshUser()
    {
        _currentUser = _httpContextAccessor.HttpContext?.User == null
            ? new CurrentUser()
            : new CurrentUser(_httpContextAccessor.HttpContext!.User);
    }
}
