using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Domain.Interface.IQuery;
using System.Net;

namespace SumXAssginment.Application.Manager.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeCommand _employeeCommand;
        private readonly IEmployeeQuery _employeeQuery;

        public EmployeeManager(IEmployeeCommand employeeCommand, IEmployeeQuery employeeQuery)
        {
            _employeeCommand = employeeCommand;
            _employeeQuery = employeeQuery;
        }

        public async Task<ResponseStatus<string>> CreateEmployeeAsync(EmployeeDto employeeDto, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeDto.FullName) || string.IsNullOrEmpty(employeeDto.EmailAddress))
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Full name and email are required",
                        Data = string.Empty
                    };
                }

                var existingEmployee = await _employeeQuery.IsEmployeeExistsByEmailAsync(employeeDto.EmailAddress, tenantId, cancellationToken);
                if (existingEmployee)
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Employee with this email already exists in your tenant",
                        Data = string.Empty
                    };
                }

                var employee = new EEmployee
                {
                    FullName = employeeDto.FullName,
                    EmailAddress = employeeDto.EmailAddress,
                    TenantId = tenantId
                };

                var employeeId = await _employeeCommand.CreateEmployeeAsync(employee, cancellationToken);

                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Employee created successfully",
                    Data = employeeId
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred while creating employee",
                    Data = string.Empty
                };
            }
        }

        public async Task<ResponseStatus<List<EmployeeDto>>> GetEmployeesAsync(string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _employeeQuery.GetEmployeesByTenantAsync(tenantId, cancellationToken);
                
                var employeeDtos = employees.Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    EmailAddress = e.EmailAddress
                }).ToList();

                return new ResponseStatus<List<EmployeeDto>>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Employees retrieved successfully",
                    Data = employeeDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<List<EmployeeDto>>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred while retrieving employees",
                    Data = new List<EmployeeDto>()
                };
            }
        }

        public async Task<ResponseStatus<EmployeeDto>> GetEmployeeByIdAsync(string employeeId, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeQuery.GetEmployeeByIdAsync(employeeId, tenantId, cancellationToken);
                if (employee == null)
                {
                    return new ResponseStatus<EmployeeDto>
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Message = "Employee not found",
                        Data = null
                    };
                }

                var employeeDto = new EmployeeDto
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    EmailAddress = employee.EmailAddress
                };

                return new ResponseStatus<EmployeeDto>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Employee retrieved successfully",
                    Data = employeeDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<EmployeeDto>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred while retrieving employee",
                    Data = null
                };
            }
        }

        public async Task<ResponseStatus<string>> UpdateEmployeeAsync(EmployeeDto employeeDto, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeDto.Id))
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Message = "Employee ID is required",
                        Data = string.Empty
                    };
                }

                var existingEmployee = await _employeeQuery.GetEmployeeByIdAsync(employeeDto.Id, tenantId, cancellationToken);
                if (existingEmployee == null)
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Message = "Employee not found",
                        Data = string.Empty
                    };
                }

                existingEmployee.FullName = employeeDto.FullName;
                existingEmployee.EmailAddress = employeeDto.EmailAddress;

                var success = await _employeeCommand.UpdateEmployeeAsync(existingEmployee, cancellationToken);
                if (!success)
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Message = "Failed to update employee",
                        Data = string.Empty
                    };
                }

                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Employee updated successfully",
                    Data = employeeDto.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred while updating employee",
                    Data = string.Empty
                };
            }
        }

        public async Task<ResponseStatus<string>> DeleteEmployeeAsync(string employeeId, string tenantId, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _employeeCommand.DeleteEmployeeAsync(employeeId, tenantId, cancellationToken);
                if (!success)
                {
                    return new ResponseStatus<string>
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Message = "Employee not found",
                        Data = string.Empty
                    };
                }

                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.OK,
                    Message = "Employee deleted successfully",
                    Data = employeeId
                };
            }
            catch (Exception ex)
            {
                return new ResponseStatus<string>
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Message = "An error occurred while deleting employee",
                    Data = string.Empty
                };
            }
        }
    }
}