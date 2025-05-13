namespace Auto_Circuit.DTO;

public class VacationDTO
{
    public string? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Reason { get; set; }
    public bool IsApproved { get; set; }
}
