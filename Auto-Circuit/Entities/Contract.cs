using Auto_Circuit.Entities.Enum;
using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.Entities;

public class Contract
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Salary { get; set; }
    public string? contractType { get; set; }
    public User? User { get; set; }
    public string? UserId { get; set; }
    public string? OwnerId { get; set; }
}