using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetExpensesAsync(Guid companyId, Expense employeeParameters, bool trackChanges);
        Task<Expense> GetExpenseAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateExpenseForCompany(Guid companyId, Expense employee);
        void DeleteExpense(Expense employee);
    }
}
