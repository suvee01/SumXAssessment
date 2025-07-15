using System.ComponentModel.DataAnnotations;

namespace SumXAssginment.Application.DTOs.Request
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = default!;

        [Required]
        public string FullName { get; set; } = default!;
    }
}