using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Manager.Interface
{
    public interface ITenantManager
    {
        Task<ResponseStatus<string>> CreateTenant(TenantDto command,CancellationToken cancellationToken);
        Task<ResponseStatus<string>> UpdateTenant(TenantDto command,CancellationToken cancellationToken);
        Task<ResponseStatus<string>> DeleteTenant(string tenantId, CancellationToken cancellationToken);
    }
}
