using Microsoft.EntityFrameworkCore;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.IQuery;
using SumXAssignment.Infrastructure.Repository;

namespace SumXAssignment.Infrastructure.Services.Query
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly AppDbContext _context;

        public EmployeeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EEmployee>> GetEmployeesByTenantAsync(string tenantId, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Where(e => e.TenantId == tenantId)
                .ToListAsync(cancellationToken);
        }

        public async Task<EEmployee?> GetEmployeeByIdAsync(string employeeId, string tenantId, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId && e.TenantId == tenantId, cancellationToken);
        }

        public async Task<bool> IsEmployeeExistsByEmailAsync(string email, string tenantId, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AnyAsync(e => e.EmailAddress == email && e.TenantId == tenantId, cancellationToken);
        }
    }
}