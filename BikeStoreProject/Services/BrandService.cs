using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreProject.Services
{
    public class BrandService : IBrandService
    {
        private readonly BikeStoreContext _context;

        public BrandService(BikeStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            return await _context.Brands
                .Select(b => new BrandDto { BrandId = b.BrandId, BrandName = b.BrandName })
                .ToListAsync();
        }

        public async Task AddBrandAsync(BrandCreateDto brandCreateDto)
        {
            var brand = new Brand { BrandName = brandCreateDto.BrandName };
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(int id, string brandName)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                brand.BrandName = brandName;
                await _context.SaveChangesAsync();
            }
        }
    }
}
