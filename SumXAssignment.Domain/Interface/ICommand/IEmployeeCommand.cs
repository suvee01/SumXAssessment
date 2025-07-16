using SumXAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumXAssignment.Domain.Interface.ICommand
{
    public interface IEmployeeCommand
    {
        Task<string> CreateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken = default);
        Task<string> UpdateEmployeeAsync(EEmployee employee, CancellationToken cancellationToken = default);
        Task<bool> DeleteEmployeeAsync(string id, CancellationToken cancellationToken = default);
    }
}