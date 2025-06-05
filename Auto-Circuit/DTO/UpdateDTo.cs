using System.ComponentModel.DataAnnotations;

namespace Auto_Circuit.DTO;

public class UpdateDTo
{
    [StringLength(50)]
    public string? UserName { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(7)]
    public string? Cin { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    public ContractDto ContractDto { get; set; }

}
