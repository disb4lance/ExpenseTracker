using Entities.Models;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IExpenseService
    {
        Task<ExpenseDto> GetExpenseAsync(Guid categoryId, Guid id, bool trackChanges);
        Task<ExpenseDto> CreateExpenseForCategoryAsync(Guid categoryId, ExpenseForCreationDto expenseForCreation, bool trackChanges);
        Task DeleteExpenseForCategoryyAsync(Guid categoryId, Guid id, bool trackChanges);
        Task UpdateExpenseForCategoryAsync(Guid categoryId, Guid id, ExpenseForUpdateDto expenseForUpdate, bool catTrackChanges, bool expTrackChanges);
        Task<(ExpenseForUpdateDto employeeToPatch, Expense employeeEntity)> GetExpenseForPatchAsync(Guid categoryId, Guid id, bool catTrackChanges, bool expTrackChanges);
        Task SaveChangesForPatchAsync(ExpenseForUpdateDto expenseToPatch, Expense expenseEntity);
    }
}
