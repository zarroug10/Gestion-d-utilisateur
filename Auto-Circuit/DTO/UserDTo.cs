using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.DTO;

public class UserDTo
{
    public string? id { get; set; }
    public string? Cin { get; set; }
    public string? Gender { get; set; }
    public string? Status { get; set; }
    public string? KidsNumber { get; set; }
    public string? Email { get; set; }
    public IList<string> roles { get; set; }
    public ContractDto Contract { get; set; }
}
