using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]

    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IServiceManager _service;
        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetCategorys")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync(trackChanges: false);
            return Ok(categories);
        }

        [HttpGet("{id:guid}", Name = "CategoryById")]
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        //[HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _service.CategoryService.GetCategoryAsync(id, trackChanges: false);
            return Ok(category);
        }

        [HttpPost(Name = "CreateCategory")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto category)
        {
            var createdCategory = await _service.CategoryService.CreateCategoryAsync(category);
            return CreatedAtRoute("CategoryById", new { id = createdCategory.Id }, createdCategory);
        }

        //[HttpGet("collection/({ids})", Name = "CategoryCollection")]
        //public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        //{
        //    var companies = _service.CompanyService.GetByIdsAsync(ids, trackChanges: false);
        //    return Ok(companies);
        //}

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CategoryForCreationDto> categoryCollection)
        {
            var result = await _service.CategoryService.CreateCategoryCollectionAsync(categoryCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.categories);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service.CategoryService.DeleteCategoryAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CategoryForUpdateDto category)
        {
            await _service.CategoryService.UpdateCategoryAsync(id, category, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Get Company Options
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

    }
}
