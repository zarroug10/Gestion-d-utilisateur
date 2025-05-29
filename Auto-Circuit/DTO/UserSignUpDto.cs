using System.ComponentModel.DataAnnotations;

using Auto_Circuit.DTO;
using Auto_Circuit.Entities.identity;

namespace Auto_Circuit.DTOs
{
    public class UserSignUpDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

        [Required]
        [StringLength(8)]
        public string CIN { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        [Required]
        [StringLength(2)]
        public int? kidsNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = UserType.Employe.ToString();

        public ContractDto? Contract { get; set; }

    }
}