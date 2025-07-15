using System.ComponentModel.DataAnnotations;

namespace SumXAssginment.Application.DTOs.Request
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}