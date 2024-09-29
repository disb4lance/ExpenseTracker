using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class ExpenseService : IExpenseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ExpenseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ExpenseDto> GetExpenseAsync(Guid categoryId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(categoryId, trackChanges);
            var expenseDb = await _repository.Expense.GetExpenseAsync(categoryId, id, trackChanges);
            if (expenseDb == null) { }
                //throw new EmployeeNotFoundException(id);

            var expense = _mapper.Map<ExpenseDto>(expenseDb);
            return expense;
        }

        //public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(Guid companyId, LinkParameters linkParameters, bool trackChanges)
        //{
        //    if (!linkParameters.EmployeeParameters.ValidAgeRange)
        //        throw new MaxAgeRangeBadRequestException();
        //    await CheckIfCompanyExists(companyId, trackChanges);
        //    var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(companyId, linkParameters.EmployeeParameters, trackChanges);
        //    var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
        //    var links = _employeeLinks.TryGenerateLinks(employeesDto, linkParameters.EmployeeParameters.Fields, companyId, linkParameters.Context);
        //    return (linkResponse: links, metaData: employeesWithMetaData.MetaData);
        //}

        public async Task<ExpenseDto> CreateExpenseForCategoryAsync(Guid categoryId, ExpenseForCreationDto expenseForCreation, bool trackChanges)
        {
            await CheckIfCompanyExists(categoryId, trackChanges);
            var expenseEntity = _mapper.Map<Expense>(expenseForCreation);
            _repository.Expense.CreateExpenseForCompany(categoryId, expenseEntity);
            await _repository.SaveAsync();
            var expenseToReturn = _mapper.Map<ExpenseDto>(expenseEntity);
            return expenseToReturn;
        }

        public async Task DeleteExpenseForCategoryyAsync(Guid categoryId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(categoryId, trackChanges);
            var expenseDb = await GetEmployeeForCompanyAndCheckIfItExists(categoryId, id, trackChanges);
            _repository.Expense.DeleteExpense(expenseDb);
            await _repository.SaveAsync();
        }

        public async Task UpdateExpenseForCategoryAsync(Guid categoryId, Guid id, ExpenseForUpdateDto expenseForUpdate, bool catTrackChanges, bool expTrackChanges)
        {
            await CheckIfCompanyExists(categoryId, catTrackChanges);
            var expenseDb = await GetEmployeeForCompanyAndCheckIfItExists(categoryId, id, expTrackChanges);
            _mapper.Map(expenseForUpdate, expenseDb);
            await _repository.SaveAsync();
        }

        public async Task<(ExpenseForUpdateDto employeeToPatch, Expense employeeEntity)> GetExpenseForPatchAsync(Guid categoryId, Guid id, bool catTrackChanges, bool expTrackChanges)
        {
            await CheckIfCompanyExists(categoryId, catTrackChanges);
            var expenseDb = await GetEmployeeForCompanyAndCheckIfItExists(categoryId, id, expTrackChanges);
            var expenseToPatch = _mapper.Map<ExpenseForUpdateDto>(expenseDb);
            return (expenseToPatch, expenseDb);
        }

        public async Task SaveChangesForPatchAsync(ExpenseForUpdateDto expenseToPatch, Expense expenseEntity)
        {
            _mapper.Map(expenseToPatch, expenseEntity);
            await _repository.SaveAsync();
        }

        private async Task CheckIfCompanyExists(Guid categoryId, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);
            if (category is null) { }
                //throw new CompanyNotFoundException(companyId);
        }

        private async Task<Expense> GetEmployeeForCompanyAndCheckIfItExists
            (Guid categoryId, Guid id, bool trackChanges)
        {
            var expenseDb = await _repository.Expense.GetExpenseAsync(categoryId, id, trackChanges);
            if (expenseDb is null) { }
                //throw new EmployeeNotFoundException(id);

            return expenseDb;
        }
    }
}
