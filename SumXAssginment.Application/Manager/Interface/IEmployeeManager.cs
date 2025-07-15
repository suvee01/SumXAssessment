using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;

namespace SumXAssginment.Application.Manager.Interface
{
    public interface IEmployeeManager
    {
        Task<ResponseStatus<string>> CreateEmployeeAsync(EmployeeDto employeeDto, string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<List<EmployeeDto>>> GetEmployeesAsync(string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<EmployeeDto>> GetEmployeeByIdAsync(string employeeId, string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<string>> UpdateEmployeeAsync(EmployeeDto employeeDto, string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<string>> DeleteEmployeeAsync(string employeeId, string tenantId, CancellationToken cancellationToken);
    }
}