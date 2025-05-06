using Auto_Circuit.Entities;

namespace Auto_Circuit.DTO;

public class UserDTo
{
    public string? id { get; set; }
    public string? firstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public ICollection<UserProducts> UserProducts { get; set; } = [];
}
