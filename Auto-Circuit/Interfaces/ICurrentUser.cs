using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.Interfaces
{
    public interface ICurrentUser
    {
        CurrentUser User { get; }

        string UserId { get; }
    }
}
