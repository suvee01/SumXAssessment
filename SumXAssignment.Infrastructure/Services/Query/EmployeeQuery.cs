using Microsoft.EntityFrameworkCore;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.IQuery;
using SumXAssignment.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Infrastructure.Services.Query
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private readonly AppDbContext _context;

        public EmployeeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmployeeExistsAsync(string id, string tenantId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AnyAsync(e => e.Id == id && e.TenantId == tenantId, cancellationToken);
        }

        public async Task<EEmployee?> GetEmployeeByIdAsync(string id, string tenantId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId, cancellationToken);
        }

        public async Task<IEnumerable<EEmployee>> GetEmployeesByTenantIdAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Where(e => e.TenantId == tenantId)
                .ToListAsync(cancellationToken);
        }
    }
}