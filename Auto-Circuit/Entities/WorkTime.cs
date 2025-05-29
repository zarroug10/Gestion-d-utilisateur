using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.Entities;

public class WorkTime
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public bool IsApproved { get; set; }
    public bool IsPending { get; set; }
}