using SumXAssginment.Application.DTOs.Request;
using SumXAssginment.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssginment.Application.Manager.Interface
{
    public interface IUserRegistrationManager
    {
        Task<ResponseStatus<string>> RegisterUserAsync(UserRegistrationDto request, string tenantId, CancellationToken cancellationToken);
    }
}