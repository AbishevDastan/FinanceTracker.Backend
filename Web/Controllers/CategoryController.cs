using Application.Extensions.UserContext;
using Application.Services.CategoryService;
using Application.UseCases.Category;
using Application.UseCases.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserContextService _userContextService;

        public CategoryController(ICategoryService categoryService,
            IUserContextService userContextService)
        {
            _categoryService = categoryService;
            _userContextService = userContextService;
        }

        [HttpGet("categories")]
        [Authorize]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoriesByUserId()
        {
            var categories = await _categoryService.GetCategoriesByUserId(_userContextService.GetCurrentUserId());

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{userId}/category-by-user-id")]
        [Authorize]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoryByUserId(int id)
        {
            var category = await _categoryService.GetCategoryByUserId(id, _userContextService.GetCurrentUserId());

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> AddCategory(AddCategoryDto addCategoryDto)
        {
            var result = await _categoryService.AddCategory(addCategoryDto, _userContextService.GetCurrentUserId());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id)
        {
            var result = await _categoryService.UpdateCategory(updateCategoryDto, id, _userContextService.GetCurrentUserId());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id, _userContextService.GetCurrentUserId());

            if (result != true)
                return NotFound();

            return Ok(result);
        }
    }
}
