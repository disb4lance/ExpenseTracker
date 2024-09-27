using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategorysAsync(bool trackChanges);
        Task<Category> GetCategoryAsync(Guid companyId, bool trackChanges);
        void CreateCategory(Category company);
        Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteCategory(Category company);
    }
}
