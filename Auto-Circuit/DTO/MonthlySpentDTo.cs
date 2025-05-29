namespace Auto_Circuit.DTO;

public class MonthlySpentDTo
{
    public decimal TotalAmount { get; set; }
    public string? Month { get; set; }
    public int Year { get; set; }
    public string? UserId { get; set; }
    public string? Username { get; set; }
}