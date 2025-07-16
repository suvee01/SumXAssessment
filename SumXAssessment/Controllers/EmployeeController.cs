using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.DTOs.Response;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using SumXAssessment.Extensions;
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

        [HttpPost]
        public async Task<ResponseStatus<string>> CreateEmployee([FromBody] EmployeeDto request, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantIdFromClaims();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in user claims",
                    Data = ""
                };
            }

            var result = await _employeeManager.CreateEmployeeAsync(request, tenantId, cancellationToken);
            return result;
        }

        [HttpGet]
        public async Task<ResponseStatus<IEnumerable<EmployeeResponseDto>>> GetEmployees(CancellationToken cancellationToken)
        {
            var tenantId = GetTenantIdFromClaims();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<IEnumerable<EmployeeResponseDto>>
                {
                    Status = 400,
                    Message = "Tenant ID not found in user claims",
                    Data = Enumerable.Empty<EmployeeResponseDto>()
                };
            }

            var result = await _employeeManager.GetEmployeesByTenantAsync(tenantId, cancellationToken);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseStatus<EmployeeResponseDto>> GetEmployee(string id, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantIdFromClaims();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<EmployeeResponseDto>
                {
                    Status = 400,
                    Message = "Tenant ID not found in user claims",
                    Data = null!
                };
            }

            var result = await _employeeManager.GetEmployeeByIdAsync(id, tenantId, cancellationToken);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ResponseStatus<string>> UpdateEmployee(string id, [FromBody] EmployeeDto request, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantIdFromClaims();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in user claims",
                    Data = ""
                };
            }

            request.Id = id; // Ensure the ID is set from the route parameter
            var result = await _employeeManager.UpdateEmployeeAsync(request, tenantId, cancellationToken);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ResponseStatus<bool>> DeleteEmployee(string id, CancellationToken cancellationToken)
        {
            var tenantId = GetTenantIdFromClaims();
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<bool>
                {
                    Status = 400,
                    Message = "Tenant ID not found in user claims",
                    Data = false
                };
            }

            var result = await _employeeManager.DeleteEmployeeAsync(id, tenantId, cancellationToken);
            return result;
        }

        private string? GetTenantIdFromClaims()
        {
            return User.GetTenantId();
        }
    }
}