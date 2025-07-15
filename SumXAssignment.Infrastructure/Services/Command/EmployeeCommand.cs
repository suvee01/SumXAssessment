using Microsoft.EntityFrameworkCore;
using SumXAssignment.Domain.Entities;
using SumXAssignment.Domain.Interface.ICommand;
using SumXAssignment.Infrastructure.Repository;

namespace SumXAssignment.Infrastructure.Services.Command
{
    public class EmployeeCommand : IEmployeeCommand
    {
        private readonly AppDbContext _context;

        public EmployeeCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return employee.Id;
        }

        public async Task<bool> UpdateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken)
        {
            _context.Employees.Update(employee);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeId, string tenantId, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId && e.TenantId == tenantId, cancellationToken);
            
            if (employee == null) return false;

            _context.Employees.Remove(employee);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}