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
    public class EmployeeCommand : IEmployeeCommand
    {
        private readonly AppDbContext _context;

        public EmployeeCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken = default)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return employee.Id;
        }

        public async Task<bool> DeleteEmployeeAsync(string id, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<string> UpdateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken = default)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee.Id;
        }
    }
}