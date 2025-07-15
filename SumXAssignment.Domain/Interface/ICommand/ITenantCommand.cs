using SumXAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Domain.Interface.ICommand
{
    public interface ITenantCommand
    {
        Task<string> CreateTenantAsync(ETenant tenant, CancellationToken cancellationToken = default);
    }
}
