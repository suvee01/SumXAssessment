using SumXAssginment.Application.DTOs.Request;
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
    public class TenantManager : ITenantManager
    {
        private readonly ITenantCommand _tenantCommand;
        private readonly IUserCommand _userCommand;
        private readonly ITenantQuery _tenantQuery;
        public TenantManager(ITenantCommand tenantCommand,
                             IUserCommand userCommand,
                             ITenantQuery tenantQuery)
        {
            _tenantCommand = tenantCommand;
            _userCommand = userCommand;
            _tenantQuery = tenantQuery;

        }
        public async Task<ResponseStatus<string>> CreateTenant(TenantDto command, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(command.Name))
                {
                    return new ResponseStatus<string>()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Data = "",
                        Message = "Tenant name is required."

                    };
                }

                string tenantId = await _tenantQuery.GenerateNextTenantIdAsync();
                var eTenant = ParseToETenant(command, tenantId);
                await _tenantCommand.CreateTenantAsync(eTenant, cancellationToken);
                var eUser = ParseToEUser(command);
                string userId = await _userCommand.AddUserAsync(eUser, cancellationToken);
                string roleId = await _userCommand.AddTenantRoleAsync("Tenant", cancellationToken);
                await _userCommand.AddUserRoleAsync(userId, roleId, cancellationToken);
                return Response(true, "Successfully added", tenantId);
            }
            catch (Exception ex)
            {
                return Response(false, "Something went wrong", "");
            }

        }

        private ETenant ParseToETenant(TenantDto command, string tenantId)
        {
            var tenant = new ETenant()
            {
                EmailAddress = command.EmailAddress,
                Name = command.Name,
                TenantId = tenantId
            };
            return tenant;
        }

        private EUser ParseToEUser(TenantDto command)
        {
            var user = new EUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = command.EmailAddress,
                Email = command.EmailAddress,
                EmailConfirmed = true,
                TenantId = command.Id,
            };
            return user;
        }
        //public string TenantId(string isExistedTenantId)
        //{
        //    if (string.IsNullOrEmpty(isExistedTenantId))
        //    {
        //        int nextNumber = 1;
        //        if (isExistedTenantId != null && int.TryParse(isExistedTenantId.TenantId[1..], out int lastNum))
        //            nextNumber = lastNum + 1;

        //        return $"T{nextNumber}";
        //    }
        //}

        private ResponseStatus<string> Response(bool isSuccess, string message, string tenantId)
        {
            var response = new ResponseStatus<string>()
            {
                Status = isSuccess ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                Data = isSuccess ? tenantId : "",
                Message =  message 
            };
            return response;
        }
    }
}
