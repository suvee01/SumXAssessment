using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.DTOs.Response;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using System.Security.Claims;

namespace SumXAssessment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ResponseStatus<LoginResponseDto>> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var result = await _authManager.LoginAsync(loginDto, cancellationToken);
            return result;
        }

        [HttpPost("Register")]
        [Authorize(Roles = "Tenant")]
        public async Task<ResponseStatus<string>> Register([FromBody] RegisterUserDto registerDto, CancellationToken cancellationToken)
        {
            var tenantId = User.Claims.FirstOrDefault(c => c.Type == "TenantId")?.Value;
            
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in token",
                    Data = string.Empty
                };
            }

            var result = await _authManager.RegisterUserAsync(registerDto, tenantId, cancellationToken);
            return result;
        }
    }
}