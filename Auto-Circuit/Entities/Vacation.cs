using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.Entities;

public class Vacation
{
    public int Id { get; set; }
    public User? User { get; set; }
    public string? UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Reason { get; set; }
    public bool IsApproved { get; set; } = false;
}
