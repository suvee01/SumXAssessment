using SumXAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Domain.Interface.IQuery
{
    public interface IUserQuery
    {
        Task<EUser?> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<ETenant?> GetTenantByIdAsync(string tenantId, CancellationToken cancellationToken = default);
    }
}