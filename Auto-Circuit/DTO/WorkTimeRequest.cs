namespace Auto_Circuit.DTO;

public class WorkTimeRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    public string? username { get; set; }
    public bool IsApproved { get; set; }
    public bool IsPending { get; set; }
}
