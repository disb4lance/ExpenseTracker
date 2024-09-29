using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;


namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _companyService;
        private readonly Lazy<IExpenseService> _employeeService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _companyService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger, mapper));
            _employeeService = new Lazy<IExpenseService>(() => new ExpenseService(repositoryManager, logger, mapper));
            //_authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
        }
        public ICategoryService CategoryService => _companyService.Value;
        public IExpenseService ExpenseService => _employeeService.Value;
        //public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
