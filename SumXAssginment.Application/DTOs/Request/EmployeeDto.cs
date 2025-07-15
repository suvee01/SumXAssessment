using System.ComponentModel.DataAnnotations;

namespace SumXAssginment.Application.DTOs.Request
{
    public class EmployeeDto
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = default!;
    }
}