using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using System.Security.Claims;

namespace SumXAssessment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Tenant")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpPost("Create")]
        public async Task<ResponseStatus<string>> CreateEmployee([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantId();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in token",
                    Data = string.Empty
                };
            }

            var result = await _employeeManager.CreateEmployeeAsync(employeeDto, tenantId, cancellationToken);
            return result;
        }

        [HttpGet("GetAll")]
        public async Task<ResponseStatus<List<EmployeeDto>>> GetAllEmployees(CancellationToken cancellationToken)
        {
            var tenantId = GetTenantId();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<List<EmployeeDto>>
                {
                    Status = 400,
                    Message = "Tenant ID not found in token",
                    Data = new List<EmployeeDto>()
                };
            }

            var result = await _employeeManager.GetEmployeesAsync(tenantId, cancellationToken);
            return result;
        }

        [HttpGet("Get/{id}")]
        public async Task<ResponseStatus<EmployeeDto>> GetEmployeeById(string id, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantId();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<EmployeeDto>
                {
                    Status = 400,
                    Message = "Tenant ID not found in token",
                    Data = null
                };
            }

            var result = await _employeeManager.GetEmployeeByIdAsync(id, tenantId, cancellationToken);
            return result;
        }

        [HttpPut("Update")]
        public async Task<ResponseStatus<string>> UpdateEmployee([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantId();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in token",
                    Data = string.Empty
                };
            }

            var result = await _employeeManager.UpdateEmployeeAsync(employeeDto, tenantId, cancellationToken);
            return result;
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResponseStatus<string>> DeleteEmployee(string id, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantId();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in token",
                    Data = string.Empty
                };
            }

            var result = await _employeeManager.DeleteEmployeeAsync(id, tenantId, cancellationToken);
            return result;
        }

        private string? GetTenantId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "TenantId")?.Value;
        }
    }
}