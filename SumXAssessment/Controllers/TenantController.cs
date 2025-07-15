using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;

namespace SumXAssessment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantManager _manager;
        public TenantController(ITenantManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Create")]
        public async Task<ResponseStatus<string>> CreateTenant([FromBody] TenantDto command, CancellationToken cancellationToken)
        {
            var result = await _manager.CreateTenant(command, cancellationToken);
            return result;
        }
        [HttpPut("Update")]
        public async Task<ResponseStatus<string>> UpdateTenant([FromBody] TenantDto command, CancellationToken cancellationToken)
        {
            var result = await _manager.UpdateTenant(command, cancellationToken);
            return result;
        }

        [HttpDelete("Delete/{tenantId}")]
        public async Task<ResponseStatus<string>> DeleteTenant(string tenantId, CancellationToken cancellationToken)
        {
            var result = await _manager.DeleteTenant(tenantId, cancellationToken);
            return result;
        }



    }
}
