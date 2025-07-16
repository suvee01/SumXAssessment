using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.DTOs.Response;
using SumXAssginment.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Manager.Interface
{
    public interface IEmployeeManager
    {
        Task<ResponseStatus<string>> CreateEmployeeAsync(EmployeeDto request, string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<IEnumerable<EmployeeResponseDto>>> GetEmployeesByTenantAsync(string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<EmployeeResponseDto>> GetEmployeeByIdAsync(string id, string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<string>> UpdateEmployeeAsync(EmployeeDto request, string tenantId, CancellationToken cancellationToken);
        Task<ResponseStatus<bool>> DeleteEmployeeAsync(string id, string tenantId, CancellationToken cancellationToken);
    }
}