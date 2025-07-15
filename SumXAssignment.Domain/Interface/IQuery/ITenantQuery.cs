using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Domain.Interface.IQuery
{
    public interface ITenantQuery
    {
        Task<string> GenerateNextTenantIdAsync();
    }
}
