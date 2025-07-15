using SumXAssignment.Domain.Entities;

namespace SumXAssignment.Domain.Interface.IQuery
{
    public interface IEmployeeQuery
    {
        Task<List<EEmployee>> GetEmployeesByTenantAsync(string tenantId, CancellationToken cancellationToken);
        Task<EEmployee?> GetEmployeeByIdAsync(string employeeId, string tenantId, CancellationToken cancellationToken);
        Task<bool> IsEmployeeExistsByEmailAsync(string email, string tenantId, CancellationToken cancellationToken);
    }
}