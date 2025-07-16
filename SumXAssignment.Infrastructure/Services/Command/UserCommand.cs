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
        
        public UserCommand(AppDbContext context, UserManager<EUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<string> AddTenantRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            // Check if role already exists
            var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
            if (existingRole != null)
            {
                return existingRole.Id;
            }

            var role = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            await _context.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return role.Id;
        }
        public async Task<string> AddUserAsync(EUser user, CancellationToken cancellationToken)
        {
            // Generate password: "Tenant" + TenantId
            var tenant = await _context.Tenants.FindAsync(user.TenantId);
            string password = "Tenant" + tenant?.TenantId;
            
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            
            return user.Id;
        }

        public async Task AddUserRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _context.Roles.FindAsync(roleId);
            
            if (user != null && role != null)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }
    }
}
