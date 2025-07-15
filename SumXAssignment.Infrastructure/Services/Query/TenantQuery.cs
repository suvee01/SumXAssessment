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
    public class TenantQuery : ITenantQuery
    {
        private readonly AppDbContext _context;
        public TenantQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateNextTenantIdAsync()
        {
                try
                {
                    // Fetch all TenantIds into memory
                    var tenantIds = await _context.Tenants
                        .Select(x => x.TenantId)
                        .ToListAsync();

                    // Find the max numeric part in memory
                    int maxNumber = tenantIds
                        .Select(id => int.TryParse(id?[1..], out var num) ? num : 0)
                        .DefaultIfEmpty(0)
                        .Max();

                    int nextNumber = maxNumber + 1;
                    return $"T{nextNumber}";
                }
                catch (Exception)
                {
                    throw;
                }
        }

        public async Task<ETenant?> GetTenantByIdAsync(string tenantId, CancellationToken cancellationToken)
        {
            return await _context.Tenants
                .FirstOrDefaultAsync(t => t.Id == tenantId, cancellationToken);
        }

    }
}
