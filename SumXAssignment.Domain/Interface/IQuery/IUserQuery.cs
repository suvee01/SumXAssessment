using SumXAssignment.Domain.Entities;

namespace SumXAssignment.Domain.Interface.IQuery
{
    public interface IUserQuery
    {
        Task<EUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<EUser?> GetUserByIdAsync(string userId, CancellationToken cancellationToken);
        Task<string?> GetUserRoleAsync(string userId, CancellationToken cancellationToken);
        Task<bool> CheckPasswordAsync(EUser user, string password, CancellationToken cancellationToken);
    }
}