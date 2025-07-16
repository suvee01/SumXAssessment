using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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