using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.DTOs.Request
{
    public class TenantDto
    {
        public string Id { get; set; }

        [StringLength(30, ErrorMessage = "Max length")]
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }

}
