using BikeStoreProject.Dto;
using BikeStoreProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoreProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand([FromBody] BrandCreateDto brandCreateDto)
        {
            await _brandService.AddBrandAsync(brandCreateDto);
            return Ok("Record Added Successfully!!");
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] string brandName)
        {
            await _brandService.UpdateBrandAsync(id, brandName);
            return NoContent();
        }
    }
}
