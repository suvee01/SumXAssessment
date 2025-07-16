using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;
using SumXAssessment.Extensions;
using System.Security.Claims;

namespace SumXAssessment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Tenant")]
    public class UserController : ControllerBase
    {
        private readonly IUserRegistrationManager _userManager;

        public UserController(IUserRegistrationManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ResponseStatus<string>> RegisterUser([FromBody] UserRegistrationDto request, CancellationToken cancellationToken)
        {
            // Get the current tenant ID from the authenticated user's claims
            var tenantId = User.GetTenantId();
            
            if (string.IsNullOrEmpty(tenantId))
            {
                return new ResponseStatus<string>
                {
                    Status = 400,
                    Message = "Tenant ID not found in user claims",
                    Data = ""
                };
            }

            var result = await _userManager.RegisterUserAsync(request, tenantId, cancellationToken);
            return result;
        }
    }
}