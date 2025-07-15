using SumXAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Domain.Interface.ICommand
{
    public interface IUserCommand
    {
        Task<string> AddUserAsync(EUser user, CancellationToken cancellationToken);
        Task<string> AddTenantRoleAsync(string roleName, CancellationToken cancellationToken);
        Task AddUserRoleAsync(string userId, string roleId, CancellationToken cancellationToken);
        Task<string> CreateUserWithPasswordAsync(EUser user, string password, CancellationToken cancellationToken);
        Task<string?> GetRoleIdByNameAsync(string roleName, CancellationToken cancellationToken);
    }
}
