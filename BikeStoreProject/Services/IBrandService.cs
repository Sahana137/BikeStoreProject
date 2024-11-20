using BikeStoreProject.Dto;

namespace BikeStoreProject.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task AddBrandAsync(BrandCreateDto brandCreateDto);
        Task UpdateBrandAsync(int id, string brandName);
    }
}
