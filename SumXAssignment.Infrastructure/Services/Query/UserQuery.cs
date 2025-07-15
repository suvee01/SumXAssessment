using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.IQuery;
using SumXAssignment.Infrastructure.Repository;

namespace SumXAssignment.Infrastructure.Services.Query
{
    public class UserQuery : IUserQuery
    {
        private readonly AppDbContext _context;
        private readonly UserManager<EUser> _userManager;
        private readonly SignInManager<EUser> _signInManager;

        public UserQuery(AppDbContext context, UserManager<EUser> userManager, SignInManager<EUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<EUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<EUser?> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<string?> GetUserRoleAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task<bool> CheckPasswordAsync(EUser user, string password, CancellationToken cancellationToken)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }
    }
}