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
        public UserCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddTenantRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = new IdentityRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            await _context.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return role.Id;
        }
        public async Task<string> AddUserAsync(EUser user, CancellationToken cancellationToken)
        {
            await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task AddUserRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
        {
            var Identityrole = new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = roleId 
            };
            await _context.AddAsync(Identityrole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
