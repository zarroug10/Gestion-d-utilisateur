using Auto_Circuit.Entities.Enum;

namespace Auto_Circuit.DTO;

public class ContractDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Salary { get; set; }
    public string? contractType { get; set; }
    public string? ownerId { get; set; }
}
