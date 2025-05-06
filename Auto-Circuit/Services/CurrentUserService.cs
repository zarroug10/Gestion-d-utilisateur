using Auto_Circuit.Entities.identity;
using Auto_Circuit.Interfaces;

namespace Auto_Circuit.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private CurrentUser? currentUser;
    public CurrentUser User =>
         currentUser ??=
         httpContextAccessor.HttpContext?.User == null
         ? new() : new(httpContextAccessor.HttpContext!.User);

    public string UserId => User.Id;

}
