using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.Entities;

public class MonthlySpent
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public decimal TotalAmount { get; set; }
    public string? Month { get; set; }
    public int Year { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}
