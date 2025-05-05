using System.ComponentModel.DataAnnotations;

namespace Auto_Circuit.DTO;

public class UpdateDTo
{
    [StringLength(50)]
    public string? UserName { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

}
