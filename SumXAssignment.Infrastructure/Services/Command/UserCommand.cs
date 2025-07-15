using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Infrastructure.Services.Command
{
    public class UserCommand : IUserCommand
    {
        private readonly AppDbContext _context;
        private readonly UserManager<EUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserCommand(AppDbContext context, UserManager<EUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> AddTenantRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole != null)
                return existingRole.Id;

            var role = new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded ? role.Id : string.Empty;
        }

        public async Task<string> AddUserAsync(EUser user, CancellationToken cancellationToken)
        {
            await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task AddUserRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
        {
            var identityRole = new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = roleId
            };
            await _context.AddAsync(identityRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> CreateUserWithPasswordAsync(EUser user, string password, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? user.Id : string.Empty;
        }

        public async Task<string?> GetRoleIdByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role?.Id;
        }
    }
}
