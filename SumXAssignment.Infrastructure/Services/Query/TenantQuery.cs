using Microsoft.EntityFrameworkCore;
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
                //var lastTenant = await _context.Tenants.OrderByDescending(x => x.TenantId).LastOrDefaultAsync();

                //int nextNumber = 1;
                //if (lastTenant != null && int.TryParse(lastTenant.TenantId[1..], out int lastNum))
                //    nextNumber = lastNum + 1;

                //return $"T{nextNumber}";

                //var lastTenant = await _context.Tenants
                //.OrderByDescending(x =>
                //    int.TryParse(x.TenantId.Substring(1), out var num) ? num : 0)
                //.FirstOrDefaultAsync();

                //int nextNumber = 1;
                //if (lastTenant != null && int.TryParse(lastTenant.TenantId[1..], out int lastNum))
                //    nextNumber = lastNum + 1;

                //return $"T{nextNumber}";

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
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
