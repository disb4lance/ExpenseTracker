

using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {

        public ExpenseRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {

        }

        public async Task<List<Expense>> GetExpensesAsync(Guid companyId, Expense employeeParameters, bool trackChanges)
        {
            var expenses = await FindByCondition(e => e.CategoryId.Equals(companyId), trackChanges)
                                                 //.FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                                                 //.Search(employeeParameters.SearchTerm)
                                                 //.Sort(employeeParameters.OrderBy)
                                                 .ToListAsync();

            //return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
            return expenses;
        }

        public async Task<Expense> GetExpenseAsync(Guid companyId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.CategoryId.Equals(companyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateExpenseForCompany(Guid companyId, Expense employee)
        {
            employee.CategoryId = companyId;
            Create(employee);
        }

        public void DeleteExpense(Expense employee) =>
            Delete(employee);
    }
}
