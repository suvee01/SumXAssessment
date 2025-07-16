using SumXAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Domain.Interface.IQuery
{
    public interface IEmployeeQuery
    {
        Task<IEnumerable<EEmployee>> GetEmployeesByTenantIdAsync(string tenantId, CancellationToken cancellationToken = default);
        Task<EEmployee?> GetEmployeeByIdAsync(string id, string tenantId, CancellationToken cancellationToken = default);
        Task<bool> EmployeeExistsAsync(string id, string tenantId, CancellationToken cancellationToken = default);
    }
}