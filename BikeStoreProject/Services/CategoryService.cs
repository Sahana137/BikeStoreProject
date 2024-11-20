using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BikeStoreContext _context;

        public CategoryService(BikeStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto { CategoryId = c.CategoryId, CategoryName = c.CategoryName })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
            return category == null ? null : new CategoryDto { CategoryId = category.CategoryId, CategoryName = category.CategoryName };
        }

        public async Task AddCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = new Category { CategoryName = categoryCreateDto.CategoryName };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int id, string categoryName)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                category.CategoryName = categoryName;
                await _context.SaveChangesAsync();
            }
        }
    }
}
