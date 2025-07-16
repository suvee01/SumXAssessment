using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.DTOs.Response
{
    public class EmployeeResponseDto
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string TenantId { get; set; } = default!;
    }
}