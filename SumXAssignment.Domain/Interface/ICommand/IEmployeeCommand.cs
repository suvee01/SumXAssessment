using SumXAssignment.Domain.Entities;

namespace SumXAssignment.Domain.Interface.ICommand
{
    public interface IEmployeeCommand
    {
        Task<string> CreateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken);
        Task<bool> UpdateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken);
        Task<bool> DeleteEmployeeAsync(string employeeId, string tenantId, CancellationToken cancellationToken);
    }
}