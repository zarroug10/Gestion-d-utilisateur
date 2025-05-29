namespace Auto_Circuit.DTO;

public class VacationFormDTo
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Reason { get; set; }
    public bool IsApproved { get; set; }
    public bool IsPending { get; set; }
}
