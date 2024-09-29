using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObject;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Service
{
    internal class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category)
        {
            var companyEntity = _mapper.Map<Category>(category);
            _repository.Category.CreateCategory(companyEntity);
            await _repository.SaveAsync();
            var companyToReturn = _mapper.Map<CategoryDto>(companyEntity);
            return companyToReturn;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
            var companies = await _repository.Category.GetAllCategorysAsync(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CategoryDto>>(companies);
            return companiesDto;
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfItExists(categoryId, trackChanges);
            var companyDto = _mapper.Map<CategoryDto>(company);
            return companyDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null) { }
                //throw new IdParametersBadRequestException();

            var companyEntities = await _repository.Category.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != companyEntities.Count()) { }
                //throw new CollectionByIdsBadRequestException();

            var companiesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(companyEntities);
            return companiesToReturn;
        }

        public async Task<(IEnumerable<CategoryDto> categories, string ids)> CreateCategoryCollectionAsync(IEnumerable<CategoryForCreationDto> categoryCollection)
        {
            if (categoryCollection is null) { }
            //throw new CompanyCollectionBadRequest();

            var companyEntities = _mapper.Map<IEnumerable<Category>>(categoryCollection);
            foreach (var company in companyEntities)
            {
                _repository.Category.CreateCategory(company);
            }

            await _repository.SaveAsync();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CategoryDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
            return (companies: companyCollectionToReturn, ids: ids);
        }

        public async Task DeleteCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var category = await GetCompanyAndCheckIfItExists(categoryId, trackChanges);
            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Guid companyId, CategoryForUpdateDto companyForUpdate, bool trackChanges)
        {
            var companyEntity = await GetCompanyAndCheckIfItExists(companyId, trackChanges);
            _mapper.Map(companyForUpdate, companyEntity);
            await _repository.SaveAsync();
        }

        private async Task<Category> GetCompanyAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(id, trackChanges);
            if (category is null) { }
                //throw new CompanyNotFoundException(id);
            return category;
        }
    }
}
