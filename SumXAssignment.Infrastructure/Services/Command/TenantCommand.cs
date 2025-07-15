using Microsoft.AspNetCore.Identity;
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
    public class TenantCommand : ITenantCommand
    {
        private readonly AppDbContext _context;
        public TenantCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateTenantAsync(ETenant tenant, CancellationToken cancellationToken)
        {
            await _context.AddAsync(tenant, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return tenant.TenantId;
        }

    }
}
