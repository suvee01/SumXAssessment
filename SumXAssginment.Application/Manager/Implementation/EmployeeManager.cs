using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.DTOs.Response;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Domain.Interface.IQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Manager.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeCommand _employeeCommand;
        private readonly IEmployeeQuery _employeeQuery;

        public EmployeeManager(
            IEmployeeCommand employeeCommand,
            IEmployeeQuery employeeQuery)
        {
            _employeeCommand = employeeCommand;
            _employeeQuery = employeeQuery;
        }

        public async Task<ResponseStatus<string>> CreateEmployeeAsync(EmployeeDto request, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.EmailAddress))
                {
                    return Response<string>(false, "Employee name and email are required.", "");
                }

                var employee = new EEmployee
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = request.FullName,
                    EmailAddress = request.EmailAddress,
                    TenantId = tenantId
                };

                var result = await _employeeCommand.CreateEmployeeAsync(employee, cancellationToken);
                return Response<string>(true, "Employee created successfully.", result);
            }
            catch (Exception ex)
            {
                return Response<string>(false, "Something went wrong during employee creation.", "");
            }
        }

        public async Task<ResponseStatus<IEnumerable<EmployeeResponseDto>>> GetEmployeesByTenantAsync(string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _employeeQuery.GetEmployeesByTenantIdAsync(tenantId, cancellationToken);
                var employeeDtos = employees.Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress,
                    TenantId = e.TenantId
                });

                return Response<IEnumerable<EmployeeResponseDto>>(true, "Employees retrieved successfully.", employeeDtos);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<EmployeeResponseDto>>(false, "Something went wrong while retrieving employees.", Enumerable.Empty<EmployeeResponseDto>());
            }
        }

        public async Task<ResponseStatus<EmployeeResponseDto>> GetEmployeeByIdAsync(string id, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeQuery.GetEmployeeByIdAsync(id, tenantId, cancellationToken);
                if (employee == null)
                {
                    return Response<EmployeeResponseDto>(false, "Employee not found.", null!);
                }

                var employeeDto = new EmployeeResponseDto
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    EmailAddress = employee.EmailAddress,
                    TenantId = employee.TenantId
                };

                return Response<EmployeeResponseDto>(true, "Employee retrieved successfully.", employeeDto);
            }
            catch (Exception ex)
            {
                return Response<EmployeeResponseDto>(false, "Something went wrong while retrieving employee.", null!);
            }
        }

        public async Task<ResponseStatus<string>> UpdateEmployeeAsync(EmployeeDto request, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Id) || string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.EmailAddress))
                {
                    return Response<string>(false, "Employee ID, name, and email are required.", "");
                }

                var existingEmployee = await _employeeQuery.GetEmployeeByIdAsync(request.Id, tenantId, cancellationToken);
                if (existingEmployee == null)
                {
                    return Response<string>(false, "Employee not found.", "");
                }

                existingEmployee.FullName = request.FullName;
                existingEmployee.EmailAddress = request.EmailAddress;

                var result = await _employeeCommand.UpdateEmployeeAsync(existingEmployee, cancellationToken);
                return Response<string>(true, "Employee updated successfully.", result);
            }
            catch (Exception ex)
            {
                return Response<string>(false, "Something went wrong during employee update.", "");
            }
        }

        public async Task<ResponseStatus<bool>> DeleteEmployeeAsync(string id, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var exists = await _employeeQuery.EmployeeExistsAsync(id, tenantId, cancellationToken);
                if (!exists)
                {
                    return Response<bool>(false, "Employee not found.", false);
                }

                var result = await _employeeCommand.DeleteEmployeeAsync(id, cancellationToken);
                return Response<bool>(true, "Employee deleted successfully.", result);
            }
            catch (Exception ex)
            {
                return Response<bool>(false, "Something went wrong during employee deletion.", false);
            }
        }

        private ResponseStatus<T> Response<T>(bool isSuccess, string message, T data) where T : class
        {
            return new ResponseStatus<T>
            {
                Status = isSuccess ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                Data = isSuccess ? data : null!,
                Message = message
            };
        }
    }
}