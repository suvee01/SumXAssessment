using Microsoft.AspNetCore.Mvc;
using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using SumXAssginment.Application.Manager.Interface;

namespace SumXAssessment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantManager _manager;
        public TenantController(ITenantManager manager)
        {
            _manager = manager;
        }

        [HttpPost("CreateTenant")]
        public async Task<ResponseStatus<string>> CreateTenant([FromBody] TenantDto command, CancellationToken cancellationToken)
        {
            var result = await _manager.CreateTenant(command, cancellationToken);
            return result;
        }
    }
}
