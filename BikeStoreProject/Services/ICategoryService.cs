using BikeStoreProject.Dto;

namespace BikeStoreProject.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryByNameAsync(string categoryName);
        Task AddCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task UpdateCategoryAsync(int id, string categoryName);
    }
}
