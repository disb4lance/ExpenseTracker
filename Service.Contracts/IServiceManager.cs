

namespace Service.Contracts
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }
        IExpenseService ExpenseService { get; }
    }
}
