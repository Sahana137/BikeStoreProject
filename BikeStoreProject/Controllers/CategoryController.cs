using BikeStoreProject.Dto;
using BikeStoreProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoreProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("categoryname/{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            var category = await _categoryService.GetCategoryByNameAsync(categoryName);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            await _categoryService.AddCategoryAsync(categoryCreateDto);
            return Ok("Record Added Successfully!!");
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] string categoryName)
        {
            await _categoryService.UpdateCategoryAsync(id, categoryName);
            return NoContent();
        }
    }
}
